using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _iEventService;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventController(IEventService iEventService, IWebHostEnvironment hostEnvironment)
    {
        _iEventService = iEventService;
        _hostEnvironment = hostEnvironment;
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

    [HttpPut]
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
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + e.InnerException.Message);
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

    [HttpPost("upload-image/{eventId}")]
    public async Task<IActionResult> UploadImage(int eventoId)
    {
        try
        {
            var evento = await _iEventService.GetEventByIdAsync(eventoId);
            if (evento == null) return NoContent();
            
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                DeleteImage(evento.ImgUrl);
                evento.ImgUrl = SaveImage(file).ToString();
            }

            var eventoReturn = await _iEventService.Update(evento, eventoId);

            return Ok(eventoReturn);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + e.InnerException.Message);
        }
    }

    private async Task<string> SaveImage(IFormFile file)
    {
        string imageName =
            new string(value: Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
        imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}";
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/assets", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return imageName;
    }

    private void DeleteImage(string eventoImgUrl)
    {
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/assets", eventoImgUrl);
        if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
    }
}