using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaitynasLab.Auth;
using SaitynasLab.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<IdentityUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            // Search for an user by name
            var user = await _userManager.FindByNameAsync(registerUserDto.Username);
            // If a user is already exists
            if (user != null)
            {
                return BadRequest("User already exists");
            }
            var newUser = new IdentityUser
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email
            };
            // Creates an user using the userManager
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create an user");

            // Assigns a role to the new user.
            await _userManager.AddToRoleAsync(newUser, UserRoles.Musician);

            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            // Search for an user by name
            var user = await _userManager.FindByNameAsync(loginUserDto.Username);
            // If user doesn't exist
            if (user == null)
                return BadRequest("User not found");

            // Check if password is correct
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            // Password is not correct
            if (!isPasswordValid)
                return BadRequest("Bad password or username");

            // Creating the token
            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);
            return Ok(new SuccessfulLoginDto(accessToken));
        }
    }
}
