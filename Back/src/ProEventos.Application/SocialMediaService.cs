using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class SocialMediaService : ISocialMediaService
{
    private readonly ISocialMediaPersist _socialMediaPersist;
    private readonly IMapper _autoMapper;

    public SocialMediaService(ISocialMediaPersist socialMediaPersist, IMapper autoMapper)
    {
        _socialMediaPersist = socialMediaPersist;
        _autoMapper = autoMapper;
    }


    public async Task<SocialMediaDto[]> SaveOnEvent(int eventId, SocialMediaDto[] dtos)
    {
        var socialMedias = await _socialMediaPersist.GetAllSocialMediaEventAsync(eventId);
        if (socialMedias == null) return null;
        foreach (var socialMediaDto in dtos)
        {
            if (socialMediaDto.Id == 0)
            {
                await AddSocialMedia(eventId, socialMediaDto, true);
            }
            else
            {
                await UpdateOnEvent(eventId, socialMediaDto, true);
            }
        }

        var socialMediaReturn = await _socialMediaPersist.GetAllSocialMediaEventAsync(eventId);
        return _autoMapper.Map<SocialMediaDto[]>(socialMediaReturn);
    }


    public async Task<SocialMediaDto[]> SaveBySpeaker(int speakerId, SocialMediaDto[] dtos)
    {
        var socialMedias = await _socialMediaPersist.GetAllSocialMediaEventAsync(speakerId);
        if (socialMedias == null) return null;
        foreach (var socialMediaDto in dtos)
        {
            if (socialMediaDto.Id == 0)
            {
                await AddSocialMedia(speakerId, socialMediaDto, false);
            }
            else
            {
                await UpdateOnEvent(speakerId, socialMediaDto, false);
            }
        }

        var socialMediaReturn = await _socialMediaPersist.GetAllSocialMediaEventAsync(speakerId);
        return _autoMapper.Map<SocialMediaDto[]>(socialMediaReturn);
    }


    public async Task<bool> DeleteOnEvent(int eventId, int socialMediaId)
    {
        var socialMedia = await _socialMediaPersist.GetSocialMediaEventIdAsync(eventId, socialMediaId);
        if (socialMedia == null)
            throw new Exception($"Doesnt exist a lot with id {eventId} in event with id = {socialMediaId}");
        _socialMediaPersist.Delete(socialMedia);
        return await _socialMediaPersist.SaveChangesAsync();
    }


    public async Task<bool> DeleteOnSpeaker(int speakerId, int socialMediaId)
    {
        var socialMedia = await _socialMediaPersist.GetSocialMediaSpeakerIdAsync(speakerId, socialMediaId);
        if (socialMedia == null)
            throw new Exception($"Doesnt exist a lot with id {speakerId} in event with id = {socialMediaId}");
        _socialMediaPersist.Delete(socialMedia);
        return await _socialMediaPersist.SaveChangesAsync();
    }

    //#region gets
    public async Task<SocialMediaDto[]> GetAllByEventId(int eventId)
    {
        var lots = await _socialMediaPersist.GetAllSocialMediaEventAsync(eventId);
        return lots == null ? null : _autoMapper.Map<SocialMediaDto[]>(lots);
    }

    public async Task<SocialMediaDto[]> GetAllBySpeaker(int speakerId)
    {
        var socialMediae = await _socialMediaPersist.GetAllSocialMediaSpeakerAsync(speakerId);
        return socialMediae == null ? null : _autoMapper.Map<SocialMediaDto[]>(socialMediae);
    }

    public async Task<SocialMediaDto> GetBySpeakerId(int speakerId, int socialMediaId)
    {
        var socialMediae = await _socialMediaPersist.GetSocialMediaSpeakerIdAsync(speakerId, socialMediaId);
        return socialMediae == null ? null : _autoMapper.Map<SocialMediaDto>(socialMediae);
    }

    public async Task<SocialMediaDto> GetByEventId(int eventId, int socialMediaId)
    {
        var socialMediae = await _socialMediaPersist.GetSocialMediaEventIdAsync(eventId, socialMediaId);
        return socialMediae == null ? null : _autoMapper.Map<SocialMediaDto>(socialMediae);
    }
    //#endregion

    //#region Private methods

    private async Task UpdateOnEvent(int id, SocialMediaDto dto, bool isEvent)
    {
        if (isEvent)
        {
            dto.EventId = id;
            dto.SpeakerId = null;
        }
        else
        {
            dto.EventId = null;
            dto.SpeakerId = id;
        }

        var socialMedia = _autoMapper.Map<SocialMedia>(dto);
        _socialMediaPersist.Update(socialMedia);
        await _socialMediaPersist.SaveChangesAsync();
    }

    private async Task AddSocialMedia(int id, SocialMediaDto socialMedia, bool isEvent)
    {
        var socialMediaDto = _autoMapper.Map<SocialMediaDto>(socialMedia);
        if (isEvent)
        {
            socialMediaDto.EventId = id;
            socialMediaDto.SpeakerId = null;
        }
        else
        {
            socialMediaDto.EventId = null;
            socialMediaDto.SpeakerId = id;
        }

        var mappedSocialMedia = _autoMapper.Map<SocialMedia>(socialMedia);

        _socialMediaPersist.Add(mappedSocialMedia);
        await _socialMediaPersist.SaveChangesAsync();
    }
    //#endregion
}