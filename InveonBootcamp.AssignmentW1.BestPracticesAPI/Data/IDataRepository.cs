using InveonBootcamp.AssignmentW1.BestPracticesAPI.Models;

namespace InveonBootcamp.AssignmentW1.BestPracticesAPI.Data
{
    public interface IDataRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        Book Create(string title, string author);
        bool Update(int id, string title, string author);
        bool Delete(int id);
    }
}
