using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class SpeakerService : ISpeakerService
{
    private readonly ISpeakerPersist _speakerPersist;
    private readonly IMapper _autoMapper;

    public SpeakerService(ISpeakerPersist speakerPersist, IMapper autoMapper)
    {
        _speakerPersist = speakerPersist;
        _autoMapper = autoMapper;
    }

    public async Task<SpeakerDto> Add(SpeakerDto dto, int speakerId)
    {
        try
        {
            var speaker = _autoMapper.Map<Speaker>(dto);
            _speakerPersist.Add(speaker);
            if (await _speakerPersist.SaveChangesAsync())
            {
                var spkr = await _speakerPersist.GetSpeakerByIdAsync(speakerId);
                return _autoMapper.Map<SpeakerDto>(spkr);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    public async Task<SpeakerDto> Update(SpeakerDto dto, int speakerId)

    {
        var speaker = await _speakerPersist.GetSpeakerByIdAsync(speakerId);
        if (speaker == null)
        {
            return null;
        }

        _autoMapper.Map(dto, speaker);
        _speakerPersist.Update(speaker);
        if (await _speakerPersist.SaveChangesAsync())
        {
            var speakerReturn = await _speakerPersist.GetSpeakerByIdAsync(speakerId);
            return _autoMapper.Map<SpeakerDto>(speakerReturn);
        }

        return null;
    }

    public async Task<bool> Delete(int speakerId)
    {
        var speaker = await _speakerPersist.GetSpeakerByIdAsync(speakerId);
        if (speaker == null) throw new Exception($"Cant find a speaker with id: {speakerId}");
        _speakerPersist.Delete(speaker);
        return await _speakerPersist.SaveChangesAsync();
    }

    public async Task<SpeakerDto[]> GetAll()
    {
        var speakers = await _speakerPersist.GetAllSpeakersAsync();
        return speakers == null ? null : _autoMapper.Map<SpeakerDto[]>(speakers);
    }

    public async Task<SpeakerDto> GetById(int speakerId)
    {
        var speaker = await _speakerPersist.GetSpeakerByIdAsync(speakerId);
        if (speaker == null)
        {
            throw new Exception($"Cant find a speaker with id: {speakerId}");
        }

        return _autoMapper.Map<SpeakerDto>(speaker);
    }
}