using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ISpeakerPersist : IGeneralPersist
{
    Task<Speaker[]> GetAllSpeakersAsync();
    Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = true);
}