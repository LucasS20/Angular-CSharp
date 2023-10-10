namespace ProEventos.Domain;

public interface IProEventosPersistence
{
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    void DeleteRange<T>(object[] entityArray) where T : class;
    Task<bool> SaveChangesAsync();

    // EVENTOS

    Task<Event[]> GetAllEventsByThemeAsync(string tema, bool includeSpeaker);
    Task<Event[]> GetAllEventsAsync(bool includeSpeaker);
    Task<Event[]> GetEventByIdAsync(int eventoId, bool includeSpeaker);

    // SPEAKERS
    Task<Event[]> GetAllSpeakersByNameAsync(string name, bool includeEvents);
    Task<Event[]> GetAllSpeakersAsync(bool includeEvents);
    Task<Event[]> GetSpeakerByIdAsync(int eventoId, bool includeEvents);
}