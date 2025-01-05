import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import Modal from '../components/Modal';

const LoginPage = () => {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [modalTitle, setModalTitle] = useState('');
  const [modalMessage, setModalMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await login(email, password);
    if (!result.success) {
      setModalTitle('Giriş Hatası');
      setModalMessage(result.message);
      setShowModal(true);
    } else {
      navigate('/');
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
        <h2 className="mb-4 text-center">Giriş Yap</h2>
        <div className="card">
          <div className="card-body">
            <form onSubmit={handleSubmit}>
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
                <label className="form-label">Şifre</label>
                <input 
                  type="password"
                  className="form-control"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required 
                />
              </div>
              <button className="btn btn-primary w-100" type="submit">
                Giriş Yap
              </button>
            </form>
            <div className="mt-2 text-center">
              <a href="/forgot-password">Şifremi Unuttum</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
