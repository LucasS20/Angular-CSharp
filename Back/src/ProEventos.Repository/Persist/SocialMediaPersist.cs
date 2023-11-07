using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

public class SocialMediaPersist : GeneralPersist, ISocialMediaPersist
{
    private readonly ProEventosContext _context;

    public SocialMediaPersist(ProEventosContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SocialMedia> GetSocialMediaEventIdAsync(int eventoId, int socialMediaId)
    {
        IQueryable<SocialMedia> query = _context.SocialMedias;
        query = query.AsQueryable().Where((socialMedia) =>
            socialMedia.EventId == eventoId && (socialMedia.Id == socialMediaId));
        return await query.FirstOrDefaultAsync();
    }

    public async Task<SocialMedia> GetSocialMediaSpeakerIdAsync(int speakerId, int socialMediaId)
    {
        IQueryable<SocialMedia> query = _context.SocialMedias;
        query = query.AsQueryable().Where(sm => sm.SpeakerId == speakerId && sm.Id == socialMediaId);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<SocialMedia[]> GetAllSocialMediaEventAsync(int eventoId)
    {
        IQueryable<SocialMedia> queryable = _context.SocialMedias;
        queryable = queryable.AsNoTracking().Where(sm => sm.EventId == eventoId);
        return await queryable.ToArrayAsync();
    }

    public async Task<SocialMedia[]> GetAllSocialMediaSpeakerAsync(int speakerId)
    {
        IQueryable<SocialMedia> queryable = _context.SocialMedias;
        queryable = queryable.AsNoTracking().Where(sm => sm.SpeakerId == speakerId);
        return await queryable.ToArrayAsync();
    }
}