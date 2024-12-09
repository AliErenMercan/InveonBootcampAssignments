using InveonBootcamp.AssignmentW1.BestPracticesAPI.Models;
using System.Xml.Linq;

namespace InveonBootcamp.AssignmentW1.BestPracticesAPI.Data
{
    public class InMemoryDatabase : IDataRepository
    {
        private readonly List<Book> _books;
        private int _nextId = 6;

        public InMemoryDatabase()
        {
            _books = new List<Book>
            {
                new(1, "1984", "George Orwell"),
                new(2, "Brave New World", "Aldous Huxley"),
                new(3, "Fahrenheit 451", "Ray Bradbury"),
                new(4, "To Kill a Mockingbird", "Harper Lee"),
                new(5, "The Catcher in the Rye", "J.D. Salinger")
            };
        }

        public List<Book> GetAll() => _books;

        public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public Book Create(string title, string author)
        {
            var newBook = new Book(_nextId++, title, author);
            _books.Add(newBook);
            return newBook;
        }

        public bool Update(int id, string title, string author)
        {
            var book = GetById(id);
            if (book == null) return false;

            _books.Remove(book);
            _books.Add(new Book(id, title, author));
            return true;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }
    }
}


