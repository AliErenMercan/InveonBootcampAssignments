using CourseInside.Models;
using CourseInside.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCoursesAsync();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCourses([FromQuery] string? keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _courseService.SearchCoursesAsync(keyword, pageNumber, pageSize);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var result = await _courseService.GetCourseByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var result = await _courseService.AddCourseAsync(course);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            course.Id = id;
            var result = await _courseService.UpdateCourseAsync(course);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
