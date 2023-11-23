using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Paginacao;

namespace ProEventos.Persistence.Persist;

public class EventPersist : GeneralPersist, IEventPersist
{
    private readonly ProEventosContext _context;

    public EventPersist(ProEventosContext context) : base(context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }


    public async Task<PageList<Event>> GetAllEventsAsync(PageParams pageParams)
    {
        IQueryable<Event> query = _context.Events
            .Include(e => e.Batches)
            .Include(e => e.SocialMedias)
            .Include(e => e.Speaker);

        query = query.AsNoTracking().Where(e => e.Theme.ToLower().Contains(pageParams.Theme.ToLower()));
        return await PageList<Event>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Event> GetEventByIdAsync(int eventoId, bool includeSpeaker)
    {
        IQueryable<Event> query = _context.Events.Include(e => e.Batches).Include(e => e.SocialMedias);
        if (includeSpeaker)
        {
            query.AsNoTracking().Include(e => e.Speaker);
        }

        return await query.Where(e => e.Id == eventoId).FirstOrDefaultAsync();
    }
}