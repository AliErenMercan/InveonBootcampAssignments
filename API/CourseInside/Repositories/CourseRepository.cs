using CourseInside.Data;
using CourseInside.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseInside.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<Course> Courses, int TotalCount)> SearchAsync(string? keyword, int pageNumber, int pageSize)
        {
            var query = _context.Courses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(kw));
            }
            var totalCount = await query.CountAsync();
            var skip = (pageNumber - 1) * pageSize;
            var courses = await query.OrderBy(c => c.Id).Skip(skip).Take(pageSize).ToListAsync();
            return (courses, totalCount);
        }
    }
}
