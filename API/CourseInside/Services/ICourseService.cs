using CourseInside.Models;
using CourseInside.Utils;

namespace CourseInside.Services
{
    public interface ICourseService
    {
        Task<ServiceResult<IEnumerable<Course>>> GetAllCoursesAsync();
        Task<ServiceResult<PagedResult<Course>>> SearchCoursesAsync(string? keyword, int pageNumber, int pageSize);
        Task<ServiceResult<Course>> GetCourseByIdAsync(int id);
        Task<ServiceResult> AddCourseAsync(Course course);
        Task<ServiceResult> UpdateCourseAsync(Course course);
        Task<ServiceResult> DeleteCourseAsync(int id);
    }
}
