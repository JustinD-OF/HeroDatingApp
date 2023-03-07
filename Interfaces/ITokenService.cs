using HeroDatingApp.Entities;

namespace HeroDatingApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user); // Like a contract between interface and implementation
        
    }
}