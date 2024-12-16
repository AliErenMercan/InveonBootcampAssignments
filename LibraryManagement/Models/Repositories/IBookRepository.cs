using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Models.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        IEnumerable<BookListDTO> GetAllListDTO();
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(int id);
    }
}
