using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ISpeakerPersist : IGeneralPersist
{
    Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false);
    Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = true);
}