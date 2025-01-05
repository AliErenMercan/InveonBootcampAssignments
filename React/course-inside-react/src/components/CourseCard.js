import React from 'react';
import { Link } from 'react-router-dom';

const CourseCard = ({ course }) => {
  return (
    <div className="card h-100 shadow-sm">
      <div className="card-body d-flex flex-column">
        <h5 className="card-title">{course.title}</h5>
        <p className="card-text text-muted flex-grow-1">
          {course.description.substring(0, 80)}...
        </p>
        <p className="fw-bold mb-2">Fiyat: ${course.price}</p>
        <Link to={`/course/${course.id}`} className="btn btn-primary mt-auto">
          Detay
        </Link>
      </div>
    </div>
  );
};

export default CourseCard;
