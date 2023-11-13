using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persist;

public class GeneralPersist : IGeneralPersist
{
    private readonly ProEventosContext _context;

    public GeneralPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(object[] entityArray) where T : class
    {
        _context.RemoveRange(entityArray);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}