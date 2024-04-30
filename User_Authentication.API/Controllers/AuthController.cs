using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Authentication.API.CustomValidation;
using User_Authentication.API.DTO;
using User_Authentication.API.Repository;

namespace User_Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        //POST: /api/auth/Register
        [HttpPost]
        [Route("Register")]
        [validateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // User created successfully, now add roles if provided
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        // Roles added successfully
                        return Ok("User was Registered! Please Login");
                    }
                    else
                    {
                        // Failed to add roles after user creation
                        var roleErrors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                        return BadRequest($"Failed to add roles to the user. Errors: {roleErrors}");
                    }
                }
                else
                {
                    // No roles provided, still consider registration successful
                    return Ok("User was Registered! Please Login");
                }
            }
            else
            {
                // User creation failed, return detailed error message
                var userErrors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                return BadRequest($"Failed to register user. Errors: {userErrors}");
            }
        }



        //POST : /api/auth/login
        [HttpPost]
        [Route("Login")]
        [validateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
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


         // [Authorize(Roles = "Admin,User")]
        // GET: /api/auth/Users
            [HttpGet]
            [Route("Users")]
            public IActionResult GetUsers()
            {

            // Specify page size as 10
            int pageSize = 10;

            // Retrieve the first page of users
            var users = _userManager.Users
                .Take(pageSize)
                .ToList();

            return Ok(users);
        }



        // GET: /api/auth/Users/{id}
           [Authorize(Roles ="Admin,User")]
            [HttpGet]
            [Route("Users/{id}")]
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


           [Authorize(Roles ="Admin")]
            // PUT: /api/auth/Users/{id}
            [HttpPut]
            [Route("Users/{id}")]
            [validateModel]
            public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
            {
                // Retrieve user by ID
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound();
                }

                // Update user properties
                user.UserName = updateUserDTO.UserName;
                user.Email = updateUserDTO.UserName;

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


           [Authorize("Admin,User")]
            // DELETE: /api/auth/Users/{id}
            [HttpDelete]
            [Route("Users/{id}")]
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


