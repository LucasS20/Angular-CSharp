using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ISocialMediaPersist : IGeneralPersist
{
    Task<SocialMedia> GetSocialMediaEventIdAsync(int eventoId, int socialMediaId);

    Task<SocialMedia> GetSocialMediaSpeakerIdAsync(int speakerId, int socialMediaId);

    Task<SocialMedia[]> GetAllSocialMediaEventAsync(int eventoId);

    Task<SocialMedia[]> GetAllSocialMediaSpeakerAsync(int speakerId);
}