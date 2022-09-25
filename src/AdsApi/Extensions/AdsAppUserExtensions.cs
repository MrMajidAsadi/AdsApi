using Ads.Api.Dtos.V1;
using Ads.Api.Dtos.V1.Users;
using Ads.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ads.Api.Extensions;

public static class AdsAppUserExtensions
{
    public static AdsAppUser ToEntity(this UserRegisterDto userRegisterDto)
    {
        var output = new AdsAppUser();

        output.UserName = userRegisterDto.UserName;
        output.Email = userRegisterDto.UserName;

        return output;
    }

    public static UserDto ToDto(this AdsAppUser user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        return new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public static ErrorDto ToErrorDto(this IdentityError error)
    {
        ErrorDto output = new();

        output.Errors.Add($"{error.Code}: {error.Description}");

        return output;
    }

    public static ErrorDto ToErrorDtos(this IEnumerable<IdentityError> errors)
    {
        ErrorDto output = new();

        foreach (var error in errors)
        {
            output.Errors.AddRange(error.ToErrorDto().Errors);
        }

        return output;
    }

    public static UserLoginResponseDto ToUserLoginResponseDto(this AdsAppUser user, string token)
    {
        UserLoginResponseDto output = new();

        output.Token = token;
        output.User = user.ToDto();

        return output;
    }

    public static ErrorDto ToErrorDto(this SignInResult signInResult)
    {
        ErrorDto output = new();

        if (signInResult.IsLockedOut)
            output.Errors.Add("Your account is lockout.");
        
        if (signInResult.IsNotAllowed)
            output.Errors.Add("You are not allowed to sign in");
        
        return output;
    }
}