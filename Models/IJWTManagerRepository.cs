namespace Databasetest.Models
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(User users);
    }
}
