using CourseInside.Models;
using CourseInside.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _service.GetAllCoursesAsync();
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var result = await _service.GetCourseByIdAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var result = await _service.AddCourseAsync(course);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] Course course)
        {
            var result = await _service.UpdateCourseAsync(course);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _service.DeleteCourseAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
        [FromQuery] string? keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _service.SearchCoursesAsync(keyword, pageNumber, pageSize);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            // result.data -> PagedResult<Course>
            return Ok(result.data);
        }
    }
}
