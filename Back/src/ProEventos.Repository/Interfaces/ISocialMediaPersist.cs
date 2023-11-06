using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ISocialMediaPersist
{
    Task<SocialMedia> GetSocialMediaEventoIdAsync(int eventoId, int socialMediaId);

    Task<SocialMedia> GetSocialMediaSpeakerIdAsync(int speakerId, int socialMediaId);

    Task<SocialMedia[]> GetAllSocialMediaEventoAsync(int eventoId);

    Task<SocialMedia[]> GetAllSocialMediaSpeakerAsync(int speakerId);
}