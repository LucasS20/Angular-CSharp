using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

public class SpeakerPersist : GeneralPersist, ISpeakerPersist
{
    private readonly ProEventosContext _context;

    public SpeakerPersist(ProEventosContext context) : base(context)
    {
        _context = context;
    }


    public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents)
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias);
        if (includeEvents)
        {
            queryable.Include(s => s.Events);
        }

        return await queryable.AsNoTracking().ToArrayAsync();
    }

    public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias);
        if (includeEvents)
        {
            queryable.Include(s => s.Events);
        }

        return await queryable.Where(e => e.Id == speakerId).FirstOrDefaultAsync();
    }
}