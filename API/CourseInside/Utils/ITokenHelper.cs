namespace CourseInside.Utils
{
    public interface ITokenHelper
    {
        string GenerateToken(string userId, string role);
    }
}
