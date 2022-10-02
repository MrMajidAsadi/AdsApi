using Ads.Api.Dtos.V1;
using Ads.Api.Dtos.V1.Users;
using Ads.Api.Extensions;
using Ads.Core.Interfaces;
using Ads.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[Controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<AdsAppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AdsAppUser> _signInnManager;
    private readonly IUserService _userService;
    private readonly ITokenClaimService _tokenClaimService;

    public UsersController(
        UserManager<AdsAppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<AdsAppUser> signInManager,
        ITokenClaimService tokenClaimService,
        IUserService userService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInnManager = signInManager;
        _tokenClaimService = tokenClaimService;
        _userService = userService;
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var user = userRegisterDto.ToEntity();
        
        var registerResult = await _userManager.CreateAsync(user, userRegisterDto.Password);

        if (!registerResult.Succeeded)
        {
            var errorDto = registerResult.Errors.ToErrorDtos();
            return BadRequest(errorDto);
        }

        await _userService.Create(user.Id);

        return Ok(user.ToDto());
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(UserLoginResponseDto),StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var signInResult = await _signInnManager.PasswordSignInAsync(
            userLoginDto.UserName,
            userLoginDto.Password,
            userLoginDto.RememberMe,
            true
        );

        if (!signInResult.Succeeded)
            return Unauthorized(signInResult.ToErrorDto());

        var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

        var token = await _tokenClaimService.GetToken(userLoginDto.UserName);
        
        return Ok(user.ToUserLoginResponseDto(token));
    }
}