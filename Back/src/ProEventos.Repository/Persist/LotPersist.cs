using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

public class LotPersist : ILotPersist
{
    private readonly ProEventosContext _context;

    public LotPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Batch[]> GetLotsByEventId(int eventoId)
    {
        IQueryable<Batch> queryable = _context.Batches;

        queryable = queryable.AsNoTracking().Where(l => l.EventId == eventoId).OrderBy(l => l.EndDate);

        return await queryable.ToArrayAsync();
    }

    public async Task<Batch> GetLotByIdsAsync(int eventoId, int lotId)
    {
        IQueryable<Batch> queryable = _context.Batches;

        queryable = queryable.Where(l => l.EventId == eventoId && l.Id == lotId);
        return await queryable.FirstOrDefaultAsync();
    }
}