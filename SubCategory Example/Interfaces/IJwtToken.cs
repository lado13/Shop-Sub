using Shop.Model;

namespace Shop.Interfaces
{
    public interface IJwtToken
    {
        string GenerateJwtToken(User user);
    }
}
