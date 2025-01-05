import React, { useContext } from 'react';
import { CourseContext } from '../context/CourseContext';

const SearchBar = () => {
  const { keyword, setKeyword, setPageNumber } = useContext(CourseContext);

  const handleChange = (e) => {
    setKeyword(e.target.value);
    setPageNumber(1);
  };

  return (
    <div className="input-group">
      <input
        type="text"
        className="form-control"
        placeholder="Kurs Ara..."
        value={keyword}
        onChange={handleChange}
      />
    </div>
  );
};

export default SearchBar;
