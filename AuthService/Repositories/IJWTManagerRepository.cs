using AuthService.Model;

namespace AuthService
{
    public interface IJWTManagerRepository
    {
        Tokens? Authenticate(Users users);
    }
}