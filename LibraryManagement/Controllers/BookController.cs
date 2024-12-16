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
            var books = bookRepository.GetAllListDTO(); // Tüm kitapları getir
            return View(books);
        }

        public IActionResult Details(int id)
        {
            Book book = bookRepository.GetById(id).Result;
            if (book == null)
            {
                return NotFound(); // Kitap bulunamazsa 404 döndür
            }
            return View(book);
        }
    }
}
