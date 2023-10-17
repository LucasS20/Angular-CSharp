using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces;

public interface IEventService
{
    Task<Event> Add(Event model);
    Task<Event> Update(Event model,int id);
    Task<bool> Delete(int id);
    Task<Event[]> GetEventsByThemeAsync(string theme, bool includeSpeaker = false);
    Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false);
    Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}