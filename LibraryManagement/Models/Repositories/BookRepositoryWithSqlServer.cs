

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

        public IEnumerable<BookListDTO> GetAllListDTO()
        {
            return context.Books.Select(book => new BookListDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                ISBN = book.ISBN
            }).ToList();
        }

        public async Task<List<Book>> GetAll()
        {
            var books = await context.Books.ToListAsync(); // Tüm kitapları veritabanından getir
            return books;
        }

        public async Task<Book> GetById(int id)
        {
            Book book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);
            return book;
        }

        public Task<Book> Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
