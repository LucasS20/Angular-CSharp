using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface IEventPersist : IGeneralPersist
{
    Task<Event[]> GetEventsByThemeAsync(string tema, bool includeSpeaker = false);
    Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false);
    Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}