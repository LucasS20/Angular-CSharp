using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ISpeakerService
{
    Task<SpeakerDto> Add(SpeakerDto dto);

    Task<SpeakerDto> Update(SpeakerDto dto, int speakerId);

    Task<SpeakerDto[]> GetAll();

    Task<SpeakerDto> GetById(int speakerId);

    Task<bool> Delete(int speakerId);
}