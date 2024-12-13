

using LibraryManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Repositories
{
    public class BookRepositoryWithSqlServer(AppDbContext context) : IBookRepository
    {
        public Task<Book> Add(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookDTO> GetAllDTO()
        {
            return context.Books.Select(book => new BookDTO
            {
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                ISBN = book.ISBN
            }).ToList();
        }

        public Task<List<Book>> GetAll()
        {
            var books = context.Books.ToListAsync(); // Tüm kitapları veritabanından getir
            return books;
        }

        public Task<Book> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
