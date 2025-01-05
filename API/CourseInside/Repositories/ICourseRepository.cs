using CourseInside.Models;

namespace CourseInside.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task<(List<Course> Courses, int TotalCount)> SearchAsync(string? keyword, int pageNumber, int pageSize);
    }
}
