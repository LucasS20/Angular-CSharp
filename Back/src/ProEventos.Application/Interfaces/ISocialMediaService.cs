using Microsoft.EntityFrameworkCore.Diagnostics;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ISocialMediaService

{
    public Task<SocialMediaDto[]> SaveByEvent(int eventId, SocialMediaDto[] dtos);
    public Task<bool> DeleteOnEvent(int eventId, int socialMediaId);
    public Task<SocialMediaDto[]> GetAllByEvent(int eventId);
    public Task<SocialMediaDto> GetByEventId(int eventId, int socialMediaId);


    public Task<SocialMediaDto[]> SaveBySpeaker(int speakerId, SocialMediaDto[] dtos);
    public Task<bool> DeleteBySpeakerId(int speakerId, int socialMediaId);
    public Task<SocialMediaDto[]> GetAllBySpeaker(int speakerId);
    public Task<SocialMediaDto> GetBySpeakerId(int speakerId, int socialMediaId);
}