﻿using Microsoft.EntityFrameworkCore;
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


    public async Task<Speaker[]> GetAllSpeakersAsync()
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias).Include(s => s.Events)
            .ThenInclude(e => e.Batches);

        return await queryable.AsNoTracking().ToArrayAsync();
    }

    public async Task<Speaker> GetSpeakerByIdAsync(int speakerId)
    {
        IQueryable<Speaker> queryable = _context.Speakers.Include(s => s.SocialMedias).Include(s => s.Events);;

        return await queryable.Where(e => e.Id == speakerId).FirstOrDefaultAsync();
    }
}