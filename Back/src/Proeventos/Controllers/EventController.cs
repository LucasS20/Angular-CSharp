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

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            var eventos = await _iEventService.GetAllEventsAsync(true);
            if (eventos == null) return NoContent();
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
                "Error when tryng to get event by ID. Error: " + e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventDto eventModel)
    {
        try
        {
            if (!ValidaLotes(eventModel))
            {
                return BadRequest("Invalid batches dates");
            }

            if (!ValidaPreco(eventModel))
            {
                return BadRequest("Invalid batches prices");
            }

            var evento = await _iEventService.Add(eventModel);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    private bool ValidaPreco(EventDto eventModel)
    {
        var listaLotes = eventModel.Batches.ToList();
        for (var i = 0; i < listaLotes.Count - 1; i++)
        {
            var precoAtual = listaLotes[i].Price;
            var proximoPreco = listaLotes[i + 1].Price;
            if (!(precoAtual < proximoPreco))
            {
                return false;
            }
        }

        return true;
    }

    private bool ValidaLotes(EventDto evento)
    {
        var listaDeLotes = evento.Batches.ToList();
        listaDeLotes = DefineHorasParaMeiaNoite(listaDeLotes);
        for (var i = 0; i < listaDeLotes.Count - 1; i++)
        {
            if (!DataInicialMenorQueFinal(listaDeLotes[i])) continue;
            var loteAtual = listaDeLotes[i];
            var proximoLote = listaDeLotes[i + 1];
            if (!DataFinalAtualMenorQueDataInicialProximo(loteAtual, proximoLote))
            {
                return false;
            }
        }

        return DataInicialMenorQueFinal(listaDeLotes[^1]);
    }

    private static List<BatchDto> DefineHorasParaMeiaNoite(List<BatchDto> listaDeLotes)
    {
        foreach (var lote in listaDeLotes)
        {
            lote.StartDate = lote.StartDate.Date;
            lote.EndDate = lote.EndDate.Date.AddMinutes(1);
        }

        return listaDeLotes;
    }

    private bool DataFinalAtualMenorQueDataInicialProximo(BatchDto loteAtual, BatchDto proximoLote)
    {
        return loteAtual.EndDate < proximoLote.StartDate;
    }

    private static bool DataInicialMenorQueFinal(BatchDto batch)
    {
        return batch.StartDate < batch.EndDate;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EventDto model)
    {
        try
        {
            if (!ValidaLotes(model))
            {
                return BadRequest("Confira a data dos Lotes");
            }

            if (!ValidaPreco(model))
            {
                return BadRequest("Um Lote sempre deve ser mais caro que o anterior");
            }

            var evento = await _iEventService.Update(model, id);
            if (evento == null) return NoContent();
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
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