namespace Databasetest.Models
{
    public interface IJWTManagerRepository
    {
        Token Authenticate(User users);
    }
}
