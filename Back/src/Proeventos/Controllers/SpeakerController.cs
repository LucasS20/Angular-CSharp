using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakerController : ControllerBase
{
    private readonly ISpeakerService _iSpeakerService;

    public SpeakerController(ISpeakerService iSpeakerService)
    {
        _iSpeakerService = iSpeakerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpeakers()
    {
        try
        {
            var speakers = await _iSpeakerService.GetAll();
            if (speakers == null) return NoContent();
            return Ok(speakers);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when trying to get speakers. Error:" + e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var evento = await _iSpeakerService.GetById(id);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when tryng to get speaker by ID. Error: " + e.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Create(SpeakerDto dto)
    {
        try
        {
            var speaker = await _iSpeakerService.Add(dto);
            if (speaker == null) return NoContent();
            return Ok(speaker.Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + e.StackTrace);
        }
    }

    [HttpPut("{speakerId}")]
    public async Task<IActionResult> Update(int speakerId, [FromBody] SpeakerDto model)
    {
        try
        {
            var speakerDto = await _iSpeakerService.Update(model, model.Id);
            if (speakerDto == null) return NoContent();
            return Ok(speakerDto);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
        }
    }


    [HttpDelete("{speakerId}")]
    public async Task<IActionResult> Delete(int speakerId)
    {
        try
        {
            return await _iSpeakerService.Delete(speakerId)
                ? Ok(new { message = "Deleted" })
                : throw new Exception("Unexpected error when trying to delete");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Internal error while delete, Error:" + e.Message);
        }
    }
}