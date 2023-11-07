using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SocialMediaController : ControllerBase
{
    private readonly ISocialMediaService _iSocialMediaService;

    public SocialMediaController(ISocialMediaService iSocialMediaService)
    {
        _iSocialMediaService = iSocialMediaService;
    }

    [HttpGet("GetAllByEventId/{eventId}")]
    public async Task<IActionResult> GetAllByEventId(int eventId)
    {
        var socialMedias = await _iSocialMediaService.GetAllByEventId(eventId);
        return socialMedias != null ? Ok(socialMedias) : NotFound();
    }

    [HttpGet("GetAllBySpeakerId/{speakerId}")]
    public async Task<IActionResult> GetAllBySpeakerId(int speakerId)
    {
        var socialMedias = await _iSocialMediaService.GetAllBySpeaker(speakerId);
        return socialMedias != null ? Ok(socialMedias) : NotFound();
    }

    [HttpGet("GetBySpeakerId/{speakerId}/{socialMediaId}")]
    public async Task<IActionResult> GetBySpeakerId(int speakerId, int socialMediaId)
    {
        var socialMedia = await _iSocialMediaService.GetBySpeakerId(speakerId, socialMediaId);
        return socialMedia != null ? Ok(socialMedia) : NotFound();
    }

    [HttpGet("GetByEventId/{eventId}/{socialMediaId}")]
    public async Task<IActionResult> GetByEventId(int eventId, int socialMediaId)
    {
        var socialMedia = await _iSocialMediaService.GetBySpeakerId(eventId, socialMediaId);
        return socialMedia != null ? Ok(socialMedia) : NotFound();
    }

    [HttpDelete("DeleteOnSpeaker/{speakerId}/{socialMediaId}")]
    public async Task<IActionResult> DeleteOnSpeaker(int speakerId, int socialMediaId)
    {
        return await _iSocialMediaService.DeleteOnSpeaker(speakerId, socialMediaId) ? NoContent() : NotFound();
    }

    [HttpDelete("DeleteOnEvent/{eventId}/{socialMediaId}")]
    public async Task<IActionResult> DeleteOnEvent(int eventId, int socialMediaId)
    {
        return await _iSocialMediaService.DeleteOnEvent(eventId, socialMediaId) ? NoContent() : NotFound();
    }

    [HttpPut("SaveOnEvent/{speakerId}")]
    public async Task<IActionResult> SaveOnEvent(int speakerId, SocialMediaDto[] dtos)
    {
        return await _iSocialMediaService.SaveOnEvent(speakerId, dtos) != null ? Ok() : BadRequest();
    }

    [HttpPut("SaveOnSpeaker/{speakerId}")]
    public async Task<IActionResult> SaveOnSpeaker(int speakerId, SocialMediaDto[] dtos)
    {
        return await _iSocialMediaService.SaveBySpeaker(speakerId, dtos) != null ? Ok() : BadRequest();
    }   
}