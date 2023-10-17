using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class EventService : IEventService
{
    private readonly IGeneralPersist _generalPersist;
    private readonly IEventPersist _eventPersist;

    public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist)
    {
        _generalPersist = generalPersist;
        _eventPersist = eventPersist;
    }

    public async Task<Event> Add(Event model)
    {
        try
        {
            _generalPersist.Add(model);
            if (await _generalPersist.SaveChangesAsync()) return await _eventPersist.GetEventByIdAsync(model.Id);
        }


        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return null;
    }


    public async Task<Event> Update(Event model, int id)
    {
        try
        {
            var evento = await _eventPersist.GetEventByIdAsync(id);
            if (evento == null) return null;
            model.Id = evento.Id;
            _generalPersist.Update(model);
            if (await _generalPersist.SaveChangesAsync()) return await _eventPersist.GetEventByIdAsync(model.Id);
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
            else
                _generalPersist.Delete<Event>(evento);
            return await _generalPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Event[]> GetEventsByThemeAsync(string theme, bool includeSpeaker = false)
    {
        try
        {
            var events = await _eventPersist.GetEventsByThemeAsync(theme, includeSpeaker);
            if (events.Length < 1 || events == null) return null;
            return events;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false)
    {
        try
        {
            var events = await _eventPersist.GetAllEventsAsync(includeSpeaker);
            if (events.Length == 0 || events == null) return null;
            return events;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker = false)
    {
        try
        {
            var evento = _eventPersist.GetEventByIdAsync(eventoId, includeSpeaker);
            if (evento == null) return null;
            return evento;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}