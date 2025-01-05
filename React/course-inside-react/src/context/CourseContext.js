import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';
import config from '../config';

export const CourseContext = createContext();

export const CourseProvider = ({ children }) => {
  const [courses, setCourses] = useState([]);
  const [keyword, setKeyword] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(6);
  const [totalPages, setTotalPages] = useState(1);
  const [isLoading, setIsLoading] = useState(false);

  const fetchCourses = async () => {
    setIsLoading(true);
    try {
      const res = await axios.get(`${config.apiBaseUrl}/Course/search`, {
        params: {
          keyword,
          pageNumber,
          pageSize
        }
      });
      setCourses(res.data.items);
      const totalCount = res.data.totalCount;
      setTotalPages(Math.ceil(totalCount / pageSize));
    } catch (err) {
      console.error(err);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchCourses();
  }, [keyword, pageNumber]);

  return (
    <CourseContext.Provider
      value={{
        courses,
        keyword,
        setKeyword,
        pageNumber,
        setPageNumber,
        totalPages,
        isLoading
      }}
    >
      {children}
    </CourseContext.Provider>
  );
};
