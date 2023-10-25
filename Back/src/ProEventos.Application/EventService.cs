using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class EventService : IEventService
{
    private readonly IGeneralPersist _generalPersist;
    private readonly IEventPersist _eventPersist;
    private readonly IMapper _autoMapper;

    public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist, IMapper autoMapper)
    {
        _generalPersist = generalPersist;
        _eventPersist = eventPersist;
        _autoMapper = autoMapper;
    }

    public async Task<EventDto> Add(EventDto dto)
    {
        try
        {
            var evento = _autoMapper.Map<Event>(dto);
            _generalPersist.Add(evento);
            if (await _generalPersist.SaveChangesAsync())
            {
                var retorno = await _eventPersist.GetEventByIdAsync(evento.Id, false);
                return _autoMapper.Map<EventDto>(retorno);
            }
        }

        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return null;
    }


    public async Task<EventDto> Update(EventDto model, int id)
    {
        try
        {
            var _event = await _eventPersist.GetEventByIdAsync(id);
            if (_event == null)
            {
                return null;
            }

            model.Id = _event.Id;
            _autoMapper.Map(model, _event);
            _generalPersist.Update<Event>(_event);
            if (await _generalPersist.SaveChangesAsync())
            {
                var eventReturn = await _eventPersist.GetEventByIdAsync(_event.Id);
                return _autoMapper.Map<EventDto>(eventReturn);
            }

            _generalPersist.Update(model);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return null;
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var evento = await _eventPersist.GetEventByIdAsync(id);
            if (evento == null) throw new Exception("Event with this id not found");
            _generalPersist.Delete(evento);
            return await _generalPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventDto[]> GetEventsByThemeAsync(string theme, bool includeSpeaker = false)
    {
        try
        {
            var events = await _eventPersist.GetEventsByThemeAsync(theme, includeSpeaker);
            if (events.Length < 1 || events == null) return null;
            var results = _autoMapper.Map<EventDto[]>(events);
            return results;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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