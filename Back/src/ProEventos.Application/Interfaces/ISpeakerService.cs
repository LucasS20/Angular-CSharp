using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ISpeakerService
{
    Task<SpeakerDto> Add(SpeakerDto dto);

    Task<SpeakerDto> Update(SpeakerDto model, int id);

    Task<SpeakerDto[]> GetAllByEventId(int eventId);

    Task<SpeakerDto[]> GetAllBySpeakerId(int speakerId);

}