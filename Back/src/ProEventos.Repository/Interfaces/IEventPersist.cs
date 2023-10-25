using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface IEventPersist
{
    Task<Event[]> GetEventsByThemeAsync(string tema, bool includeSpeaker = false);
    Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false);
    Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}