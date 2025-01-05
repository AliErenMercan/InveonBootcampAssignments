import React, { useContext } from 'react';
import { CourseContext } from '../context/CourseContext';
import SearchBar from '../components/SearchBar';
import CourseCard from '../components/CourseCard';

const HomePage = () => {
  const { courses, pageNumber, setPageNumber, totalPages, isLoading } = useContext(CourseContext);

  return (
    <div className="home-page">
      <div className="mb-4 text-center">
        <h2 className="mb-3">Kurs Listesi</h2>
        <SearchBar />
      </div>
      {isLoading ? (
        <div className="text-center">
          <div className="spinner-border" role="status"/>
          <p className="mt-2">Yükleniyor...</p>
        </div>
      ) : (
        <>
          {courses.length === 0 ? (
            <p className="text-center fs-5">Hiç kurs bulunamadı.</p>
          ) : (
            <div className="row">
              {courses.map((course) => (
                <div className="col-md-4 mb-4" key={course.id}>
                  <CourseCard course={course} />
                </div>
              ))}
            </div>
          )}
          <div className="d-flex justify-content-center align-items-center mt-3">
            <button
              className="btn btn-secondary me-2"
              disabled={pageNumber <= 1}
              onClick={() => setPageNumber((prev) => prev - 1)}
            >
              Önceki
            </button>
            <span>Sayfa {pageNumber} / {totalPages}</span>
            <button
              className="btn btn-secondary ms-2"
              disabled={pageNumber >= totalPages}
              onClick={() => setPageNumber((prev) => prev + 1)}
            >
              Sonraki
            </button>
          </div>
        </>
      )}
    </div>
  );
};

export default HomePage;
