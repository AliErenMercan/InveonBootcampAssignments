using CourseInside.Models;

namespace CourseInside.Services
{
    public interface ICourseService
    {
        Task<ServiceResult<IEnumerable<Course>>> GetAllCoursesAsync();
        Task<ServiceResult<Course>> GetCourseByIdAsync(int id);
        Task<ServiceResult> AddCourseAsync(Course course);
        Task<ServiceResult> UpdateCourseAsync(Course course);
        Task<ServiceResult> DeleteCourseAsync(int id);
    }
}
