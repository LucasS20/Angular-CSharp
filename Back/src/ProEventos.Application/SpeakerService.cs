using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class SpeakerService : ISpeakerService
{
    private readonly IGeneralPersist _generalPersist;
    private readonly ISpeakerPersist _speakerPersist;
    private readonly IMapper _autoMapper;

    public SpeakerService(IGeneralPersist generalPersist, ISpeakerPersist speakerPersist, IMapper autoMapper)
    {
        _generalPersist = generalPersist;
        _speakerPersist = speakerPersist;
        _autoMapper = autoMapper;
    }

    public async Task<SpeakerDto> Add(SpeakerDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<SpeakerDto> Update(SpeakerDto model, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SpeakerDto[]> GetAllByEventId(int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<SpeakerDto[]> GetAllBySpeakerId(int speakerId)
    {
        throw new NotImplementedException();
    }

    public Task<SpeakerDto> GetSpeakerByIdAsync(int eventoId, bool includeSpeaker = false)
    {
        throw new NotImplementedException();
    }
}