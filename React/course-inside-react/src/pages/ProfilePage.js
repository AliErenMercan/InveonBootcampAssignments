import React, { useContext, useEffect, useState } from 'react';
import axios from 'axios';
import config from '../config';
import { AuthContext } from '../context/AuthContext';
import Modal from '../components/Modal';

const ProfilePage = () => {
  const { user } = useContext(AuthContext);
  const [profile, setProfile] = useState(null);
  const [error, setError] = useState('');
  const [isEditing, setIsEditing] = useState(false);
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');

  const [showModal, setShowModal] = useState(false);
  const [modalTitle, setModalTitle] = useState('');
  const [modalMessage, setModalMessage] = useState('');

  const fetchProfile = async () => {
    if (!user?.token) return;
    try {
      const res = await axios.get(`${config.apiBaseUrl}/User/profile`, {
        headers: { Authorization: `Bearer ${user.token}` }
      });
      setProfile(res.data);
      setIsEditing(false);
      setEmail(res.data.email);
      setName(res.data.name);
      setOldPassword('');
      setNewPassword('');
    } catch (err) {
      setError(err.response?.data || 'Profile could not be loaded.');
    }
  };

  useEffect(() => {
    fetchProfile();
  }, [user]);

  if (!user?.token) {
    return (
      <div className="alert alert-warning">
        Lütfen giriş yapınız.
      </div>
    );
  }

  const handleUpdate = async (e) => {
    e.preventDefault();
    setError('');
    try {
      const userId = profile.userId;
      const updateData = {
        email,
        name,
        oldPassword,
        newPassword
      };
      const response = await axios.put(`${config.apiBaseUrl}/User/${userId}`, updateData, {
        headers: { Authorization: `Bearer ${user.token}` }
      });
      setModalTitle('Bilgi');
      setModalMessage(response.data);
      setShowModal(true);
      await fetchProfile();
    } catch (err) {
      setModalTitle('Hata');
      setModalMessage(err.response?.data || 'Güncelleme başarısız.');
      setShowModal(true);
    }
  };

  const closeModal = () => {
    setShowModal(false);
    setModalMessage('');
  };

  return (
    <div className="row justify-content-center">
      <Modal
        show={showModal}
        title={modalTitle}
        message={modalMessage}
        onClose={closeModal}
      />
      <div className="col-md-8">
        <h2 className="mb-4">Profilim</h2>
        {error && <div className="alert alert-danger">{error}</div>}
        {!profile ? (
          <div>Yükleniyor...</div>
        ) : (
          <div className="card">
            <div className="card-body">
              {!isEditing ? (
                <>
                  <p><strong>Email:</strong> {profile.email}</p>
                  <p><strong>Ad:</strong> {profile.name}</p>
                  <p><strong>Rol:</strong> {profile.role}</p>
                  <hr />
                  <h4>Satın Alınan Kurslar</h4>
                  {profile.purchasedCourses.length === 0 ? (
                    <p>Henüz herhangi bir kurs satın almadınız.</p>
                  ) : (
                    <ul className="list-group mb-3">
                      {profile.purchasedCourses.map((pc) => (
                        <li className="list-group-item" key={pc.orderId}>
                          {pc.courseTitle} - {new Date(pc.purchaseDate).toLocaleDateString()}
                        </li>
                      ))}
                    </ul>
                  )}
                  <button className="btn btn-primary" onClick={() => setIsEditing(true)}>
                    Profili Düzenle
                  </button>
                </>
              ) : (
                <form onSubmit={handleUpdate}>
                  <div className="mb-3">
                    <label className="form-label">Email</label>
                    <input 
                      type="email"
                      className="form-control"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Ad</label>
                    <input 
                      type="text"
                      className="form-control"
                      value={name}
                      onChange={(e) => setName(e.target.value)}
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Eski Şifre</label>
                    <input 
                      type="password"
                      className="form-control"
                      value={oldPassword}
                      onChange={(e) => setOldPassword(e.target.value)}
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label">Yeni Şifre</label>
                    <input 
                      type="password"
                      className="form-control"
                      value={newPassword}
                      onChange={(e) => setNewPassword(e.target.value)}
                    />
                  </div>
                  <div className="d-flex gap-2">
                    <button type="submit" className="btn btn-success">
                      Kaydet
                    </button>
                    <button
                      type="button"
                      className="btn btn-secondary"
                      onClick={() => {
                        setEmail(profile.email);
                        setName(profile.name);
                        setOldPassword('');
                        setNewPassword('');
                        setIsEditing(false);
                      }}
                    >
                      İptal
                    </button>
                  </div>
                </form>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default ProfilePage;
