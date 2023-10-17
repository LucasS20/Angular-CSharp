using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ISpeakerPersist
{
    // SPEAKERS
    Task<Speaker> GetSpeakersByNameAsync(string name, bool includeEvents);
    Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents);
    Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents);
}