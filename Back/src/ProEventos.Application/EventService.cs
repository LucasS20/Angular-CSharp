using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class EventService : IEventService
{
    private readonly IEventPersist _eventPersist;
    private readonly IMapper _autoMapper;

    public EventService(IEventPersist eventPersist, IMapper autoMapper)
    {
        _eventPersist = eventPersist;
        _autoMapper = autoMapper;
    }

    public async Task<EventDto> Add(EventDto dto)
    {
        var evento = _autoMapper.Map<Event>(dto);
        _eventPersist.Add(evento);
        if (await _eventPersist.SaveChangesAsync())
        {
            var retorno = await _eventPersist.GetEventByIdAsync(evento.Id);
            return _autoMapper.Map<EventDto>(retorno);
        }

        return null;
    }


    public async Task<EventDto> Update(EventDto model, int id)
    {


        var evento = await _eventPersist.GetEventByIdAsync(id);
        if (evento == null)
        {
            return null;
        }

        var a = _autoMapper.Map(model, evento);

        a.Batches = _autoMapper.Map<IEnumerable<Batch>>(model.Batches);
        _eventPersist.Update(a);
        if (await _eventPersist.SaveChangesAsync())
        {
            return model;
        }

        return null;
    }



    

    public async Task<bool> Delete(int id)
    {
        var evento = await _eventPersist.GetEventByIdAsync(id);
        if (evento == null) throw new Exception("Event with this id not found");
        _eventPersist.Delete(evento);
        return await _eventPersist.SaveChangesAsync();
    }

    public async Task<EventDto[]> GetEventsByThemeAsync(string theme, bool includeSpeaker = false)
    {
        var events = await _eventPersist.GetEventsByThemeAsync(theme, includeSpeaker);
        if (events.Length < 1) return null;
        var results = _autoMapper.Map<EventDto[]>(events);
        return results;
    }

    public async Task<EventDto[]> GetAllEventsAsync(bool includeSpeaker = false)
    {
        try
        {
            var events = await _eventPersist.GetAllEventsAsync(includeSpeaker);
            var result = _autoMapper.Map<EventDto[]>(events);

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventDto> GetEventByIdAsync(int eventoId, bool includeSpeaker = false)
    {
        try
        {
            var evento = await _eventPersist.GetEventByIdAsync(eventoId, includeSpeaker);
            if (evento == null) return null;
            var result = _autoMapper.Map<EventDto>(evento);

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}