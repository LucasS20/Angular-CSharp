using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotController : ControllerBase
{
    private readonly ILotService _iLotService;

    public LotController(ILotService iLotService)
    {
        _iLotService = iLotService;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetAll(int eventId)
    {
        try
        {
            var lots = await _iLotService.GetLotsByEventIdAsync(eventId);
            if (lots == null) return NoContent();
            return Ok(lots);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error when trying to get events. Error:" + e.InnerException.Message);
        }
    }
    
    [HttpPut("{eventId}")]
    public async Task<IActionResult> Put(int eventId, LotDto[] model)
    {
        try
        {
            var _event = await _iLotService.Put(eventId, model);
            if (_event == null) return NoContent();
            return Ok(_event);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.InnerException.Message);
        }
    }

    [HttpDelete("{eventId}/{lotId}")]
    public async Task<IActionResult> Delete(int eventId, int lotId)
    {
        try
        {
            var lot = await _iLotService.GetLotByIdsAsync(eventId, lotId);
            if (lot == null) return NoContent();

            return await _iLotService.Delete(eventId, lotId)
                ? Ok(new { message = "Deleted" })
                : throw new Exception($"Unexpected error when trying to delete a lot with id: {lotId} in event with id: {eventId} ");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Internal error while delete, Error:" + e.InnerException.Message);
        }
    }
}