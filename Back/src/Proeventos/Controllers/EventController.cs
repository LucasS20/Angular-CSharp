using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _iEventService;

    public EventController(IEventService iEventService)
    {
        _iEventService = iEventService;
    }

    [HttpGet(Name = "GetEvent")]
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            var _events = await _iEventService.GetAllEventsAsync(true);
            if (_events == null) return NoContent();
            return Ok(_events);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when trying to get events. Error:" + e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var evento = await _iEventService.GetEventByIdAsync(id);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when tryng to get event by ID. Error: " + e.Message);
        }
    }

    [HttpGet("theme/{theme}")]
    public async Task<IActionResult> GetByTheme(string theme)
    {
        try
        {
            var evento = await _iEventService.GetEventsByThemeAsync(theme, true);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when tryng to get event by ID. Error: " + e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventDto eventModel)
    {
        try
        {
            var evento = await _iEventService.Add(eventModel);
            if (evento == null) return NoContent();
            return Ok(evento.Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EventDto model)
    {
        try
        {
            var evento = await _iEventService.Update(model, id);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _iEventService.Delete(id)
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