using CourseInside.Models;
using CourseInside.Repositories;
using CourseInside.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace CourseInside.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CourseService> _logger;
        private readonly IMemoryCache _memoryCache;

        public CourseService(ICourseRepository courseRepository, ILogger<CourseService> logger, IMemoryCache memoryCache)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<ServiceResult<IEnumerable<Course>>> GetAllCoursesAsync()
        {
            if (!_memoryCache.TryGetValue("AllCourses", out IEnumerable<Course> courses))
            {
                courses = await _courseRepository.GetAllCoursesAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set("AllCourses", courses, cacheEntryOptions);
            }
            _logger.LogInformation("GetAllCoursesAsync called, count={count}", courses.Count());
            return ServiceResult<IEnumerable<Course>>.Ok(courses);
        }

        public async Task<ServiceResult<PagedResult<Course>>> SearchCoursesAsync(string? keyword, int pageNumber, int pageSize)
        {
            var cacheKey = $"SearchCourses_{keyword}_{pageNumber}_{pageSize}";
            if (!_memoryCache.TryGetValue(cacheKey, out PagedResult<Course> cachedResult))
            {
                var (courses, totalCount) = await _courseRepository.SearchAsync(keyword, pageNumber, pageSize);
                cachedResult = new PagedResult<Course>
                {
                    Items = courses,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, cachedResult, cacheEntryOptions);
            }
            _logger.LogInformation("SearchCoursesAsync {keyword} page={pageNumber}", keyword, pageNumber);
            return ServiceResult<PagedResult<Course>>.Ok(cachedResult);
        }

        public async Task<ServiceResult<Course>> GetCourseByIdAsync(int id)
        {
            var cacheKey = $"Course_{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out Course course))
            {
                course = await _courseRepository.GetByIdAsync(id);
                if (course == null)
                {
                    _logger.LogWarning("Course not found id={id}", id);
                    return ServiceResult<Course>.Fail("Kurs bulunamadı!");
                }
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(cacheKey, course, cacheEntryOptions);
            }
            return ServiceResult<Course>.Ok(course);
        }

        public async Task<ServiceResult> AddCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
            _logger.LogInformation("Course added {title}", course.Title);
            _memoryCache.Remove("AllCourses");
            return ServiceResult.Ok("Kurs başarıyla eklendi.");
        }

        public async Task<ServiceResult> UpdateCourseAsync(Course course)
        {
            var existing = await _courseRepository.GetByIdAsync(course.Id);
            if (existing == null)
            {
                _logger.LogWarning("UpdateCourseAsync: not found id={id}", course.Id);
                return ServiceResult.Fail("Kurs bulunamadı!");
            }
            existing.Title = course.Title;
            existing.Description = course.Description;
            existing.Price = course.Price;
            existing.Category = course.Category;
            await _courseRepository.UpdateAsync(existing);
            _logger.LogInformation("Course updated id={id}", existing.Id);
            _memoryCache.Remove("AllCourses");
            _memoryCache.Remove($"Course_{course.Id}");
            return ServiceResult.Ok("Kurs başarıyla güncellendi.");
        }

        public async Task<ServiceResult> DeleteCourseAsync(int id)
        {
            var existing = await _courseRepository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("DeleteCourseAsync: not found id={id}", id);
                return ServiceResult.Fail("Kurs bulunamadı!");
            }
            await _courseRepository.DeleteAsync(existing);
            _memoryCache.Remove("AllCourses");
            _memoryCache.Remove($"Course_{id}");
            return ServiceResult.Ok("Kurs başarıyla silindi.");
        }
    }
}
