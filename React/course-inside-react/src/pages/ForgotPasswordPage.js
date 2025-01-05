import React, { useState } from 'react';
import axios from 'axios';
import config from '../config';
import Modal from '../components/Modal';

const ForgotPasswordPage = () => {
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [modalTitle, setModalTitle] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setMessage('');
    if (newPassword !== confirmPassword) {
      setModalTitle('Hata');
      setError('Şifreler eşleşmiyor');
      setShowModal(true);
      return;
    }
    try {
      const response = await axios.post(`${config.apiBaseUrl}/User/reset-password`, {
        email,
        newPassword
      });
      setMessage(response.data);
      setModalTitle('Başarılı');
      setShowModal(true);
    } catch (err) {
      setModalTitle('Hata');
      setError(err.response?.data || 'Şifre sıfırlama başarısız');
      setShowModal(true);
    }
  };

  const closeModal = () => {
    setShowModal(false);
    setError('');
    setMessage('');
  };

  return (
    <div className="row justify-content-center mt-5">
      <Modal
        show={showModal}
        title={modalTitle}
        message={error || message}
        onClose={closeModal}
      />
      <div className="col-md-6">
        <h2 className="mb-4 text-center">Şifremi Unuttum</h2>
        <div className="card">
          <div className="card-body">
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label className="form-label">Kayıtlı Email</label>
                <input 
                  type="email"
                  className="form-control"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Yeni Şifre</label>
                <input 
                  type="password"
                  className="form-control"
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                  required
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Yeni Şifre (Tekrar)</label>
                <input 
                  type="password"
                  className="form-control"
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  required
                />
              </div>
              <button className="btn btn-primary w-100" type="submit">Şifreyi Sıfırla</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ForgotPasswordPage;
