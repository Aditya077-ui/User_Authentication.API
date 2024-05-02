using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using User_Authentication.Web.Models;

namespace User_Authentication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string JwtToken;
        private readonly string BaseUrl = "https://localhost:7098/api/auth/";

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;

            if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JwtToken", out var token) is true)
                JwtToken = token;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new();

            string requestUrl = BaseUrl + "users";


            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<UserDetail>>(responseBody);

                return View(data.ToList());
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {

            HttpClient httpClient = new();

            string requestUrl = BaseUrl + $"users/{id}";


            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserDetail>(responseBody);

                return View(data);
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterDto dto)
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

                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            HttpClient httpClient = new();

            string requestUrl = BaseUrl + $"users/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserDetail>(responseBody);

                var result = new RegisterDto()
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    Name = data.Name,
                    DepartmentName = data.DepartmentName,
                    City = data.City,
                    State = data.State,
                };
                
                return View(result);
            }
            else
            {
                return RedirectToAction("Unauthorize", "Home");
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            HttpClient httpClient = new();

            string requestUrl = BaseUrl + $"users/{id}";

            var jsonRequest = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                return View();
            }
            else
            {
                return RedirectToAction("Unauthorize", "Home");
            }
        }


        [HttpPost, ActionName("Delete")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpClient httpClient = new();

            string requestUrl = BaseUrl + $"users/{id}";


            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserDetail>(responseBody);
                if (data != null)
                {
                    //_placesRepository.Delete(place);
                    //await _placesRepository.CommitAsync();
                }

                return View(data);
            }
            else
            {
                return View();
            }

        }

    }
}
