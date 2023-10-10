using ProEventos.Domain;

namespace ProEventos.Persistence;

public class ProEventosPersistence : IProEventosPersistence
{
    private readonly ProEventosContext _context;

    public ProEventosPersistence(ProEventosContext context)
    {
        this._context = context;
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

    public Task<Event[]> GetAllEventsByThemeAsync(string tema, bool includeSpeaker)
    {
        throw new NotImplementedException();
    }

    public Task<Event[]> GetAllEventsAsync(bool includeSpeaker = false)
    {
        throw new NotImplementedException();
    }

    public Task<Event[]> GetEventByIdAsync(int eventoId, bool includeSpeaker)
    {
        throw new NotImplementedException();
    }

    public Task<Event[]> GetAllSpeakersByNameAsync(string name, bool includeEvents)
    {
        throw new NotImplementedException();
    }

    public Task<Event[]> GetAllSpeakersAsync(bool includeEvents)
    {
        throw new NotImplementedException();
    }

    public Task<Event[]> GetSpeakerByIdAsync(int eventoId, bool includeEvents)
    {
        throw new NotImplementedException();
    }
}