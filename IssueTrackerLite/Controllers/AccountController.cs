using AutoMapper;
using IssueTrackerLite.Data;
using IssueTrackerLite.DTOs;
using IssueTrackerLite.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerLite.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExistsAsync(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }

        var user = mapper.Map<AppUser>(registerDto);
        user.UserName = registerDto.Username.ToLower();


        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var userDto = new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };

        return userDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x =>
            x.NormalizedEmail == loginDto.Email.ToUpper());

        if (user is null || user.UserName is null)
        {
            return Unauthorized("Invalid email");
        }

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result)
        {
            return Unauthorized();
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExistsAsync(string username)
    {
        return await userManager.Users.AnyAsync(x =>
            x.NormalizedUserName == username.ToUpper());
    }
}
