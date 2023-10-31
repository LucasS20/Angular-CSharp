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

    public async Task<Batch[]> GetLotsByEventId(int eventoId)
    {
        try
        {
            IQueryable<Batch> queryable = _context.Batches;

            queryable = queryable.AsNoTracking().Where(l => l.EventId == eventoId);

            return queryable.ToArray();
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }
    }

    public async Task<Batch> GetLotByIdsAsync(int eventoId, int lotId)
    {
        IQueryable<Batch> queryable = _context.Batches;

        queryable = queryable.Where(l => l.EventId == eventoId && l.Id == lotId);
        return await queryable.FirstOrDefaultAsync();
    }
}