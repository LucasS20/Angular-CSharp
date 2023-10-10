using Microsoft.AspNetCore.Mvc;
using Proeventos.Data;
using ProEventos.Domain;

namespace Proeventos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly DataContext _context;

    public EventController(DataContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetEvent")]
    public IEnumerable<Event> Get()
    {
        return _context.Eventos.ToList();
    }

    [HttpGet("{id}")]
    public Event GetById(int id)
    {
        return _context.Eventos.Find(id);
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