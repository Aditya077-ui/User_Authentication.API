using Newtonsoft.Json.Linq;

namespace User_Authentication.Web.Services
{
    public class AuthService : IAuthService
    {
        private static IHttpContextAccessor _contextAccessor;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsLoggedIn()
        {
            var JwtToken = string.Empty;
            if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JwtToken", out var token) is true)
                JwtToken = token;

            return JwtToken == null;
        }
    }

}
