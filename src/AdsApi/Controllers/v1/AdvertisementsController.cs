using System.Security.Claims;
using Ads.Api.Dtos.V1.Advertisements;
using Ads.Api.Dtos.V1.Users;
using Ads.Api.Extensions;
using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;
using Ads.Core.Interfaces;
using Ads.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[Controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdvertisementsController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IRepository<Advertisement> _advertisementRepository;
    private readonly UserManager<AdsAppUser> _userManager;

    public AdvertisementsController(
        IAdvertisementService advertisementService,
        IRepository<Advertisement> advertisementRepository,
        UserManager<AdsAppUser> userManager)
    {
        _advertisementService = advertisementService;
        _advertisementRepository = advertisementRepository;
        _userManager = userManager;
    }

    private async Task<UserDto> GetUserAsDto(User user)
    {
        var identityUser = await _userManager.FindByIdAsync(user?.IdentityId);
        return identityUser.ToDto();
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody] CreateAdvertisementDto createAdvertisementDto)
    {
        var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
        if (userId is null) return Unauthorized();

        Dictionary<int, bool> pictureIds = new();
        foreach (var pictureDto in createAdvertisementDto.Pictures!)
        {
            pictureIds.Add(pictureDto.PictureId, pictureDto.IsMain);
        }

        var advertisement = await _advertisementService.Create(
            createAdvertisementDto.Title,
            createAdvertisementDto.Description,
            int.Parse(userId.Value),
            createAdvertisementDto.ShortDescription,
            createAdvertisementDto.CategoryIds,
            pictureIds);

        return Ok(createAdvertisementDto);
    }

    [AllowAnonymous]
    [HttpGet("advertisementId")]
    public virtual async Task<IActionResult> GetSingle(int advertisementId)
    {
        var advertisement = await _advertisementRepository.GetAll()
            .Include(a => a.Pictures)
                .ThenInclude(ap => ap.Picture)
            .Include(a => a.User)
            .SingleOrDefaultAsync(a => a.Id == advertisementId);

        if (advertisement is null) return NotFound();

        var dto = advertisement.ToDto();
        dto.User = await GetUserAsDto(advertisement.User);

        return Ok(dto);
    }


    [HttpDelete("advertisementId")]
    public virtual async Task<IActionResult> Delete(int advertisementId)
    {
        var userId = 0;
        int.TryParse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value, out userId);
        if (userId is 0) return Unauthorized();

        var advertisement = await _advertisementRepository.Get(advertisementId);
        if (advertisement is null || advertisement.UserId != userId) return NotFound();

        await _advertisementService.Delete(advertisement);

        return NoContent();
    }
}