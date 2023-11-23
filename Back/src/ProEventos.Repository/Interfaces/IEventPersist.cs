using ProEventos.Domain;
using ProEventos.Persistence.Paginacao;

namespace ProEventos.Persistence.Interfaces;

public interface IEventPersist : IGeneralPersist
{
    Task<PageList<Event>> GetAllEventsAsync( PageParams pageParams);
    Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}