using stocks.Models;

namespace stocks.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}