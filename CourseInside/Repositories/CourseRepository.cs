using CourseInside.Data;
using CourseInside.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Courses
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(List<Course> Courses, int TotalCount)> SearchCoursesAsync(string? keyword, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Arama ifadesini normalize edelim (örnek basit: Title içinde Contains)
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lowerKeyword = keyword.Trim().ToLower();
                // Title’da arama
                query = query.Where(c => c.Title.ToLower().Contains(lowerKeyword));
            }

            // Toplam kayıt sayısı (filtre uygulanmış haliyle)
            var totalCount = await query.CountAsync();

            // Sayfalama
            var skip = (pageNumber - 1) * pageSize;
            var courses = await query
                .OrderBy(c => c.Id) // sıralama opsiyonel
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (courses, totalCount);
        }
    }
}
