using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface IEventService
{
    Task<EventDto> Add(EventDto dto);
    Task<EventDto> Update(EventDto model, int id);
    Task<bool> Delete(int id);
    Task<EventDto[]> GetEventsByThemeAsync(string theme, bool includeSpeaker = false);
    Task<EventDto[]> GetAllEventsAsync(bool includeSpeaker = false);
    Task<EventDto> GetEventByIdAsync(int eventoId, bool includeSpeaker = false);
}