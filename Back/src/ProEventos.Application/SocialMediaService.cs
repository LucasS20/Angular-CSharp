using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Persist;

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


    public async Task<SocialMediaDto[]> SaveByEvent(int eventId, SocialMediaDto[] dtos)
    {
        var socialMedias = await _socialMediaPersist.GetAllSocialMediaEventoAsync(eventId);
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

        var socialMediaReturn = _socialMediaPersist.GetAllSocialMediaEventoAsync(eventId);
        return _autoMapper.Map<SocialMediaDto[]>(socialMediaReturn);
    }


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
    

    public async Task<SocialMediaDto[]> SaveBySpeaker(int speakerId, SocialMediaDto[] dtos)
    {
        var socialMedias = await _socialMediaPersist.GetAllSocialMediaEventoAsync(speakerId);
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

        var socialMediaReturn = _socialMediaPersist.GetAllSocialMediaEventoAsync(speakerId);
        return _autoMapper.Map<SocialMediaDto[]>(socialMediaReturn);
    }


    private async Task AddSocialMedia(int id, SocialMediaDto socialMedia, bool isEvent)
    {
        var socialMediaDto = _autoMapper.Map<SocialMediaDto>(socialMedia);
        if (isEvent)
        {
            socialMedia.EventId = id;
            socialMedia.SpeakerId = null;
        }
        else
        {
            socialMedia.EventId = null;
            socialMedia.SpeakerId = id;
        }

        socialMedia.EventId = id;
        _socialMediaPersist.Add(socialMediaDto);
        await _socialMediaPersist.SaveChangesAsync();
    }

    public async Task<bool> DeleteOnEvent(int eventId, int socialMediaId)
    {
        var socialMedia = await _socialMediaPersist.GetSocialMediaEventoIdAsync(eventId, socialMediaId);
        if (socialMedia == null)
            throw new Exception($"Doesnt exist a lot with id {eventId} in event with id = {socialMediaId}");
        _socialMediaPersist.Delete(socialMedia);
        return await _socialMediaPersist.SaveChangesAsync();
    }

    public Task<SocialMediaDto[]> GetAllByEvent(int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<SocialMediaDto> GetByEventId(int eventId, int socialMediaId)
    {
        throw new NotImplementedException();
    }


    public Task<bool> DeleteBySpeakerId(int speakerId, int socialMediaId)
    {
        throw new NotImplementedException();
    }

    public Task<SocialMediaDto[]> GetAllBySpeaker(int speakerId)
    {
        throw new NotImplementedException();
    }

    public Task<SocialMediaDto> GetBySpeakerId(int speakerId, int socialMediaId)
    {
        throw new NotImplementedException();
    }
}