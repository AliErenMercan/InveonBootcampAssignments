namespace LibraryManagement.Models.DTOs
{
    public class BookListDTO
    {
        public int Id { get; set; } // Primary Key
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int PublicationYear { get; set; }
        public string? ISBN { get; set; }
    }
}
