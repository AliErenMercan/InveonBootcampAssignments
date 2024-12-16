using LibraryManagement.Attributes;
using LibraryManagement.Models;
using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagement.Controllers
{
    public class BookController(IBookRepository bookRepository) : Controller
    {
        [PermissionAuthorize("Viewer")]
        [HttpGet]
        public IActionResult ListBooks()
        {
            var books = bookRepository.GetAllListDTO();
            return View(books);
        }

        [PermissionAuthorize("Viewer")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            Book book = bookRepository.GetById(id).Result;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
