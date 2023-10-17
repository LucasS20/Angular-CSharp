using Microsoft.AspNetCore.Mvc;
using ProEventos.Application;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;

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
    public async Task<IActionResult> Get()
    {
        try
        {
            var eventos = await _iEventService.GetAllEventsAsync(true);
            if (eventos == null) return NotFound("no events found");
            return Ok(eventos);
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
            if (evento == null) return BadRequest("Cannot find a event with ID: " + id);
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
            if (evento == null) return NotFound("Cannot find a event with theme: " + theme);
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when tryng to get event by ID. Error: " + e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Event eventModel)
    {
        try
        {
            var evento = await _iEventService.Add(eventModel);
            if (evento == null) return BadRequest("Error when trying to add a new event");
            return Ok(evento.Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Event model)
    {
        try
        {
            var evento = await _iEventService.Update(model, id);
            if (evento == null) return BadRequest("Error when trying to update");
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error, Message: " + e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _iEventService.Delete(id)
                ? Ok("Deleted")
                : BadRequest("Error then trying to delete with ID: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Internal error while delete, Error:" + e.Message);
        }
    }
}