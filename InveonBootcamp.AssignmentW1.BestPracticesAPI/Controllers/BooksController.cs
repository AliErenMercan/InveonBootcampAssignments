using InveonBootcamp.AssignmentW1.BestPracticesAPI.Data;
using InveonBootcamp.AssignmentW1.BestPracticesAPI.DTOs;
using InveonBootcamp.AssignmentW1.BestPracticesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InveonBootcamp.AssignmentW1.BestPracticesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly CachingService _cachingService;

        public BooksController(IDataRepository repository, CachingService cachingService)
        {
            _repository = repository;
            _cachingService = cachingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            const string cacheKey = "books";
            var cachedBooks = await _cachingService.GetAsync<List<BookDTO>>(cacheKey);

            if (cachedBooks != null)
            {
                var paginatedResult = Paginate(cachedBooks, page, pageSize);
                return Ok(paginatedResult);
            }

            var books = _repository.GetAll();
            if (!books.Any())
            {
                return NotFound(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "No books found",
                    Detail = "The library currently has no books."
                });
            }

            var bookDTOs = books.Select(b => new BookDTO(b.Title, b.Author)).ToList();
            await _cachingService.SetAsync(cacheKey, bookDTOs, TimeSpan.FromMinutes(10));

            var result = Paginate(bookDTOs, page, pageSize);
            return Ok(result);
        }

        private object Paginate<T>(List<T> items, int page, int pageSize)
        {
            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var paginatedItems = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = paginatedItems
            };
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {

            string cacheKey = $"book_{id}";
            var cachedBook = await _cachingService.GetAsync<BookDTO>(cacheKey);

            if (cachedBook != null)
            {
                return Ok(cachedBook);
            }

            var book = _repository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            BookDTO entity = new BookDTO(book.Title, book.Author);
            await _cachingService.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(10));
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookDTO bookDTO)
        {
            if (string.IsNullOrWhiteSpace(bookDTO.Title) || string.IsNullOrWhiteSpace(bookDTO.Author))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Invalid book data",
                    Detail = "Title and author are required."
                });
            }

            var newBook = _repository.Create(bookDTO.Title, bookDTO.Author);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, null);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] BookDTO bookDTO)
        {
            if (!_repository.Update(id, bookDTO.Title, bookDTO.Author))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_repository.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}



