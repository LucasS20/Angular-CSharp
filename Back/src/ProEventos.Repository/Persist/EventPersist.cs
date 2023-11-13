﻿using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;

namespace ProEventos.Persistence.Persist;

public class EventPersist
{
    private readonly ProEventosContext _context;

    public EventPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Event[]> GetEventsByThemeAsync(string tema, bool includeSpeaker)
    {
        IQueryable<Event> query = _context.Events
            .Include(e => e.Lots)
            .Include(e => e.SocialMedias);

        if (includeSpeaker)
        {
            query.Include(e => e.Speaker);
        }

        query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Theme.ToLower().Contains(tema.ToLower()));

        return await query.ToArrayAsync();
    }

    public async Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false)
    {
        IQueryable<Event> query = _context.Events
            .Include(e => e.Lots)
            .Include(e => e.SocialMedias);
        query = query.OrderBy(e => e.Id);
        if (includeSpeaker)
        {
            query.AsNoTracking().Include(e => e.Speaker);
        }

        return await query.ToArrayAsync();
    }

    public async Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker)
    {
        IQueryable<Event> query = _context.Events.Include(e => e.Lots).Include(e => e.SocialMedias);
        if (includeSpeaker)
        {
            query.AsNoTracking().Include(e => e.Speaker);
        }

        return await query.Where(e => e.Id == eventoId).FirstOrDefaultAsync();
    }
}