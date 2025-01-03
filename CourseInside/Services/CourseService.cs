using CourseInside.Models;
using CourseInside.Repositories;
using CourseInside.Utils;

namespace CourseInside.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository repository, ILogger<CourseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<Course>>> GetAllCoursesAsync()
        {
            var courses = await _repository.GetAllCoursesAsync();
            return ServiceResult<IEnumerable<Course>>.Success(courses);
        }

        public async Task<ServiceResult<Course>> GetCourseByIdAsync(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return ServiceResult<Course>.Failure("Course not found");
            }

            return ServiceResult<Course>.Success(course);
        }

        public async Task<ServiceResult> AddCourseAsync(Course course)
        {
            await _repository.AddCourseAsync(course);
            return ServiceResult.Success("Course added successfully");
        }

        public async Task<ServiceResult> UpdateCourseAsync(Course course)
        {
            await _repository.UpdateCourseAsync(course);
            return ServiceResult.Success("Course updated successfully");
        }

        public async Task<ServiceResult> DeleteCourseAsync(int id)
        {
            await _repository.DeleteCourseAsync(id);
            return ServiceResult.Success("Course deleted successfully");
        }

        public async Task<ServiceResult<PagedResult<Course>>> SearchCoursesAsync(string? keyword, int pageNumber, int pageSize)
        {
            var (courses, totalCount) = await _repository.SearchCoursesAsync(
                keyword, pageNumber, pageSize
            );

            var pagedResult = new PagedResult<Course>
            {
                Items = courses,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return ServiceResult<PagedResult<Course>>.Success(pagedResult);
        }
    }
}
