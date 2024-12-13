using LibraryManagement.Models;
using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagement.Controllers
{
    public class BookController(IBookRepository bookRepository) : Controller
    {
        [HttpGet]
        public IActionResult ListBooks()
        {
            var books = bookRepository.GetAllDTO(); // Tüm kitapları getir
            return View(books);
        }
    }
}
