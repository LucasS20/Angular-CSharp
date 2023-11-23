using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Exceptions;
using ProEventos.Application.Interfaces;
using Proeventos.Extentions;
using ProEventos.Persistence.Paginacao;

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

    [HttpGet]
    public async Task<IActionResult> GetAllEvents([FromQuery] PageParams pageParams)
    {
        try
        {
            var eventos = await _iEventService.GetAllEventsAsync(pageParams);
            if (eventos == null) return NoContent();
            
            Response.AddPagination(eventos.CurrentPage,eventos.PageSize,eventos.TotalCount,eventos.TotalPages);
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
            return Ok(evento);
        }

        catch (PrecoInvalidoException e)
        {
            return BadRequest(e);
        }
        catch (LoteDataInvalidaException e)
        {
            return BadRequest(e);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
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
        catch (PrecoInvalidoException e)
        {
            return BadRequest(e.Message);
        }
        catch (LoteDataInvalidaException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
                "Internal error while delete, Error:" + e);
        }
    }
}