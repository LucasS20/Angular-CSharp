using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

class SpeakerPersist : ISpeakerPersist
{
    private ProEventosContext _context;

    public SpeakerPersist(ProEventosContext context)
    {
        _context = context;
    }


    public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents)
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias);
        if (includeEvents)
        {
            queryable.Include(s => s.EventSpeakers).ThenInclude(es => es.Event);
        }

        return await queryable.OrderBy(e => e.Id).ToArrayAsync();
    }

    public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias);
        if (includeEvents)
        {
            queryable.Include(s => s.EventSpeakers).ThenInclude(es => es.Event);
        }

        return await queryable.Where(e => e.Id == speakerId).FirstOrDefaultAsync();
    }

    public async Task<Speaker> GetSpeakersByNameAsync(string name, bool includeEvents = false)
    {
        IQueryable<Speaker> query = _context.Speakers.Include(S => S.SocialMedias);
        if (includeEvents)
        {
            query.Include(s => s.EventSpeakers).ThenInclude(es => es.Event);
        }

        return await query.OrderBy(s => s.Id).Where(s => s.Name.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();
    }
}