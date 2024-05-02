using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Authentication.API.CustomValidation;
using User_Authentication.API.DTO;
using User_Authentication.API.Model;
using User_Authentication.API.Repository;

namespace User_Authentication.API.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }


        //POST: /api/auth/Register
        [HttpPost]
        [Route("register")]
        [validateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName,
                DepartmentName = registerRequestDTO.DepartmentName,
                Name = registerRequestDTO.Name,
                State = registerRequestDTO.State,
                City = registerRequestDTO.City,
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // User created successfully, now add roles if provided
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {

                    // Add roles to the user
                    identityResult = await _userManager.AddToRolesAsync(applicationUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        // Roles added successfully or no roles provided
                        return Ok("User was Registered! Please Login");
                    }
                }
            }

            // If user creation failed
            return BadRequest("Failed to register user.");
        }




        //POST : /api/auth/login
        [HttpPost]
        [Route("login")]
        [validateModel]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (user != null)
            {
                var checkpasswordresult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkpasswordresult)
                {
                    //get roles for this user
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token
                        var JWTToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var Response = new LoginResponseDTO
                        {
                            JWTToken = JWTToken,
                        };
                        return Ok(Response);
                    }


                }
            }

            return BadRequest("Username or password is incorrect");
        }




        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {

            int pagesize = 5;

            // Retrieve users based on pagination parameters
            var users = _userManager.Users.Take(pagesize).ToList();
            return Ok(users);
        }


        // GET: /api/auth/Users/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            // Retrieve user by ID
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        // PUT: /api/auth/Users/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("users/{id}")]
        //[validateModel]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            // Retrieve user by ID
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

      
            // Update user properties if provided in the updateUserDTO
            if (!string.IsNullOrEmpty(updateUserDTO.UserName))
            {
                user.UserName = updateUserDTO.UserName;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.Name))
            {
                user.Name = updateUserDTO.Name;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.DepartmentName))
            {
                user.DepartmentName = updateUserDTO.DepartmentName;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.City))
            {
                user.City = updateUserDTO.City;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.State))
            {
                user.State = updateUserDTO.State;
            }

            // Update user in the database
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Failed to update user: {errors}");
            }
        }



        [Authorize(Roles = "Admin,User")]
        // DELETE: /api/auth/Users/{id}
        [HttpDelete]
        [Route("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // Retrieve user by ID
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Delete user from the database
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Failed to delete user: {errors}");
            }
        }
    }


}


