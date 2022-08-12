namespace Databasetest.Models
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
