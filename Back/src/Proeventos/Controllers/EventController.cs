using Microsoft.AspNetCore.Mvc;
using Proeventos.Models;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private IEnumerable<Evento> _event = new[]
    {
        new Evento()
        {
            EventoId = 1,
            Theme = "Angular and .Net5",
            Local = "Belo Horizonte",
            Lote = "1º Lote",
            NumberOfPeoples = 250,
            Data = DateTime.Now.AddDays(2).ToString(),
            imgURL = "img.png"
        },
        new Evento()
        {
            EventoId = 2,
            Theme = "Angular e novidades",
            Local = "Sao paulo",
            Lote = "2º Lote",
            NumberOfPeoples = 2520,
            Data = DateTime.Now.AddDays(3).ToString(),
            imgURL = "foto.png"
        }
    };

    public EventController()
    {
    }

    [HttpGet(Name = "GetEvent")]
    public IEnumerable<Evento> Get()
    {
        return _event;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetById(int id)
    {
        return _event.Where(evento => evento.EventoId == id);
    }

    [HttpPost]
    public string Post()
    {
        return "exemplo post";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
        return "Exemplo de PUT com id = " + id;
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return "Exemplo de Delete com id = " + id;
    }
}