using Ads.Api.Dtos.V1.Pictures;
using Ads.Api.Extensions;
using Ads.Core.Entities;
using Ads.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controller;

[ApiController]
[Route("api/v1/[Controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PicturesController : ControllerBase
{
    private readonly IRepository<Picture> _pictureRepository;

    public PicturesController(IRepository<Picture> pictureRepository)
    {
        _pictureRepository = pictureRepository;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post(CreatePictureDto createPictureDto)
    {
        var userId = 0;
        int.TryParse(User.Identity?.Name, out userId);
        if (userId == 0) return Unauthorized();
        
        var picture = new Picture(
            createPictureDto.MimeType,
            createPictureDto.VirtualPath,
            userId,
            createPictureDto.AltAttribute,
            createPictureDto.TitleAttribute);
        
        await _pictureRepository.Add(picture);

        return CreatedAtAction(
            nameof(GetSingle),
            new { pictureId = picture.Id },
            picture.ToDto());
    }

    [AllowAnonymous]
    [HttpGet("{pictureId:int}")]
    public virtual async Task<IActionResult> GetSingle(int pictureId)
    {
        var picture = await _pictureRepository.Get(pictureId);

        if (picture is null) return NotFound();

        return Ok(picture.ToDto());
    }

    [HttpPut("{pictureId:int}")]
    public virtual async Task<IActionResult> UpdatePut(int pictureId, [FromBody] UpdatePictureDto updatePictureDto)
    {
        if (pictureId <= 0 || pictureId != updatePictureDto.Id) return BadRequest();

        var picture = await _pictureRepository.Get(pictureId);

        var currentUserId = 0;
        int.TryParse(User.Identity?.Name, out currentUserId);

        if (picture is null || picture.UserId != currentUserId) return NotFound();

        picture.UpdateDetails(
            updatePictureDto.MimeType,
            updatePictureDto.VirtualPath,
            updatePictureDto.AltAttribute,
            updatePictureDto.TitleAttribute);

        await _pictureRepository.Update(picture);

        return Ok(picture.ToDto());
    }

    [HttpDelete("{pictureId:int}")]
    public virtual async Task<IActionResult> Delete(int pictureId)
    {
        var picture = await _pictureRepository.Get(pictureId);

        var currentUserId = 0;
        int.TryParse(User.Identity?.Name, out currentUserId);

        if (picture?.UserId != currentUserId) return NotFound();

        await _pictureRepository.Remove(picture);

        return NoContent();
    }
}