using Microsoft.AspNetCore.Identity;

namespace User_Authentication.API.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
