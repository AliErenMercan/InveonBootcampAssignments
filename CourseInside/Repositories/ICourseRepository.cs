using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<(List<Course> Courses, int TotalCount)> SearchCoursesAsync(string? keyword, int pageNumber, int pageSize);
    }
}
