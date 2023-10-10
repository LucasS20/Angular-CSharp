using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly ProEventos.Domain.ProEventosContext _context;

    public EventController(ProEventos.Domain.ProEventosContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetEvent")]
    public IEnumerable<Event> Get()
    {
        return _context.Events.ToList();
    }

    [HttpGet("{id}")]
    public Event GetById(int id)
    {
        return _context.Events.Find(id);
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