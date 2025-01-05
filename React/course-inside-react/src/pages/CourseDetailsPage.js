import React, { useContext, useEffect, useState } from 'react';
import axios from 'axios';
import config from '../config';
import { useParams } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import Modal from '../components/Modal';

const CourseDetailsPage = () => {
  const { id } = useParams();
  const { user } = useContext(AuthContext);
  const [course, setCourse] = useState(null);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [showPaymentModal, setShowPaymentModal] = useState(false);
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showLoginModal, setShowLoginModal] = useState(false);
  const [cardNumber, setCardNumber] = useState('');
  const [cardHolder, setCardHolder] = useState('');
  const [expiryDate, setExpiryDate] = useState('');
  const [cvv, setCvv] = useState('');

  useEffect(() => {
    const fetchCourse = async () => {
      try {
        setLoading(true);
        const { data } = await axios.get(`${config.apiBaseUrl}/Course/${id}`);
        setCourse(data);
      } catch (err) {
        setError(err.response?.data || 'Kurs bulunamadı.');
      } finally {
        setLoading(false);
      }
    };
    fetchCourse();
  }, [id]);

  const openPaymentModal = () => {
    if (!user?.token) {
      setShowLoginModal(true);
      return;
    }
    setShowPaymentModal(true);
  };

  const handlePayment = async () => {
    if (!cardNumber || !cardHolder || !expiryDate || !cvv) {
      alert('Lütfen kart bilgilerini eksiksiz giriniz.');
      return;
    }
    try {
      setLoading(true);
      await new Promise((r) => setTimeout(r, 2000));
      await axios.post(`${config.apiBaseUrl}/Order/buy`, { courseId: parseInt(id) }, {
        headers: { Authorization: `Bearer ${user.token}` }
      });
      setShowPaymentModal(false);
      setShowSuccessModal(true);
    } catch (err) {
      setError(err.response?.data || 'Satın alma başarısız.');
    } finally {
      setLoading(false);
    }
  };

  if (error) {
    return <div className="alert alert-danger mt-3">{error}</div>;
  }

  if (loading && !course) {
    return (
      <div className="text-center mt-4">
        <div className="spinner-border" role="status"></div>
        <p className="mt-2">Yükleniyor...</p>
      </div>
    );
  }

  if (!course) {
    return null;
  }

  return (
    <div className="row justify-content-center">
      <div className="col-lg-8">
        <h2 className="mb-4">Kurs Detayı</h2>
        <div className="card">
          <div className="card-body">
            <h3>{course.title}</h3>
            <p className="text-muted">{course.description}</p>
            <p><strong>Fiyat:</strong> ${course.price}</p>
            <p><strong>Kategori:</strong> {course.category}</p>
            <button className="btn btn-success mt-3" onClick={openPaymentModal}>
              Satın Al
            </button>
          </div>
        </div>
      </div>
      <Modal
        show={showLoginModal}
        title="Giriş Yapmalısınız"
        message="Satın alma işlemi için lütfen giriş yapınız."
        onClose={() => setShowLoginModal(false)}
      />
      {showPaymentModal && (
        <div className="modal show fade d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Ödeme Bilgileri</h5>
                <button type="button" className="btn-close" onClick={() => setShowPaymentModal(false)}></button>
              </div>
              <div className="modal-body">
                <div className="mb-3">
                  <label>Kart Numarası</label>
                  <input
                    type="text"
                    className="form-control"
                    value={cardNumber}
                    onChange={(e) => setCardNumber(e.target.value)}
                    placeholder="XXXX XXXX XXXX XXXX"
                  />
                </div>
                <div className="mb-3">
                  <label>Kart Sahibi</label>
                  <input
                    type="text"
                    className="form-control"
                    value={cardHolder}
                    onChange={(e) => setCardHolder(e.target.value)}
                    placeholder="Ad Soyad"
                  />
                </div>
                <div className="mb-3">
                  <label>Son Kullanma Tarihi (MM/YY)</label>
                  <input
                    type="text"
                    className="form-control"
                    value={expiryDate}
                    onChange={(e) => setExpiryDate(e.target.value)}
                    placeholder="12/24"
                  />
                </div>
                <div className="mb-3">
                  <label>CVV</label>
                  <input
                    type="password"
                    className="form-control"
                    value={cvv}
                    onChange={(e) => setCvv(e.target.value)}
                    placeholder="123"
                  />
                </div>
              </div>
              <div className="modal-footer">
                <button className="btn btn-secondary" onClick={() => setShowPaymentModal(false)}>
                  İptal
                </button>
                <button className="btn btn-success" onClick={handlePayment}>
                  {loading ? 'Ödeme İşleniyor...' : 'Ödemeyi Onayla'}
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
      {showSuccessModal && (
        <div className="modal show fade d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Tebrikler!</h5>
                <button type="button" className="btn-close" onClick={() => setShowSuccessModal(false)}></button>
              </div>
              <div className="modal-body">
                <p>Satın alma işleminiz başarıyla tamamlandı.</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-primary" onClick={() => setShowSuccessModal(false)}>
                  Kapat
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default CourseDetailsPage;
