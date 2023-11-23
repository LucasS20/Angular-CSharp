using ProEventos.Application.Dtos;
using ProEventos.Persistence.Paginacao;

namespace ProEventos.Application.Interfaces;

public interface IEventService
{
    Task<EventDto> Add(EventDto dto);

    Task<EventDto> Update(EventDto model, int id);
    Task<bool> Delete(int id);
    Task<PageList<EventDto>> GetAllEventsAsync(PageParams  pageParams);
    Task<EventDto> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}