using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using User_Authentication.Web.Models;

namespace User_Authentication.Web.Controllers
{
    public class Authentication : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string BaseUrl = "https://localhost:7098/api/auth/";

        public Authentication(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            ViewBag.IsLoggedIn = true;
            _contextAccessor.HttpContext?.Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            HttpClient httpClient = new();

            string requestUrl = BaseUrl + "login";

            var jsonRequest = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);
                _contextAccessor.HttpContext?.Response.Cookies.Append("JwtToken", tokenResponse.JWTToken);

                var roles = tokenResponse.JWTToken;
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            HttpClient httpClient = new();

            string requestUrl = BaseUrl + "register";

            var jsonRequest = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
    }
}
