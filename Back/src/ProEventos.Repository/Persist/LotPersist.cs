using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

public class LotPersist : ILotPersist
{
    private ProEventosContext _context;

    public LotPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Lot[]> GetLotsByEventId(int eventoId)
    {
        IQueryable<Lot> queryable = _context.Lots;

        queryable = queryable.AsNoTracking().Where(l => l.EventId == eventoId);

        return queryable.ToArray();
    }

    public async Task<Lot> GetLotByIdsAsync(int eventoId, int lotId)
    {
        IQueryable<Lot> queryable = _context.Lots;

        queryable = queryable.Where(l => l.EventId == eventoId && l.Id == lotId);
        return await queryable.FirstOrDefaultAsync();
    }
}