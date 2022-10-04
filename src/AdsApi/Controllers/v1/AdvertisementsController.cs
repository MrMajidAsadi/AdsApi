using System.Net;
using System.Security.Claims;
using Ads.Api.Dtos.V1;
using Ads.Api.Dtos.V1.Advertisements;
using Ads.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[Controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdvertisementsController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;

    public AdvertisementsController(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
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
}