using System.Reflection;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int PublicationYear { get; set; }
        public string? ISBN { get; set; }
        public string? Genre { get; set; }
        public string? Publisher { get; set; }
        public int PageCount { get; set; }
        public string? Language { get; set; }
        public string? Summary { get; set; }
        public int AvailableCopies { get; set; }

        public Book Create(int id, string title, string author, int publicationYear, string ISBN, string Genre, 
            string publisher, int pageCount, string language, string summary, int availableCopies)
        {
            Book createdBook = new Book()
            {
                Id = id,
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                ISBN = ISBN,
                Genre = Genre,
                Publisher = publisher,
                PageCount = pageCount,
                Language = language,
                Summary = summary,
                AvailableCopies = availableCopies
            };
            return createdBook;
        }
    }
}
