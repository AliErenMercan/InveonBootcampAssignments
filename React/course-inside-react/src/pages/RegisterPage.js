import React, { useState } from 'react';
import axios from 'axios';
import config from '../config';
import { useNavigate } from 'react-router-dom';
import Modal from '../components/Modal';

const RegisterPage = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPass, setConfirmPass] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [modalTitle, setModalTitle] = useState('');
  const [modalMessage, setModalMessage] = useState('');

  const handleRegister = async (e) => {
    e.preventDefault();
    if (password !== confirmPass) {
      setModalTitle('Hata');
      setModalMessage('Şifreler eşleşmiyor');
      setShowModal(true);
      return;
    }
    try {
      const { data } = await axios.post(`${config.apiBaseUrl}/User/register`, {
        email,
        name,
        password
      });
      setModalTitle('Bilgi');
      setModalMessage(data);
      setShowModal(true);
      setTimeout(() => navigate('/login'), 1500);
    } catch (err) {
      setModalTitle('Hata');
      setModalMessage(err.response?.data || 'Kayıt başarısız.');
      setShowModal(true);
    }
  };

  const closeModal = () => {
    setShowModal(false);
    setModalMessage('');
  };

  return (
    <div className="row justify-content-center mt-5">
      <Modal
        show={showModal}
        title={modalTitle}
        message={modalMessage}
        onClose={closeModal}
      />
      <div className="col-md-6 col-lg-4">
        <h2 className="mb-4 text-center">Kayıt Ol</h2>
        <div className="card">
          <div className="card-body">
            <form onSubmit={handleRegister}>
              <div className="mb-3">
                <label className="form-label">Email</label>
                <input 
                  type="email"
                  className="form-control"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required 
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Ad Soyad</label>
                <input 
                  type="text"
                  className="form-control"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required 
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Şifre</label>
                <input 
                  type="password"
                  className="form-control"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required 
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Şifre (Tekrar)</label>
                <input 
                  type="password"
                  className="form-control"
                  value={confirmPass}
                  onChange={(e) => setConfirmPass(e.target.value)}
                  required 
                />
              </div>
              <button className="btn btn-primary w-100" type="submit">
                Kayıt Ol
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
