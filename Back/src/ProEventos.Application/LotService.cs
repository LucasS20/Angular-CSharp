using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class LotService : ILotService
{
    private readonly IGeneralPersist _generalPersist;
    private readonly ILotPersist _lotPersist;
    private readonly IMapper _autoMapper;

    public LotService(IGeneralPersist generalPersist, ILotPersist lotPersist, IMapper autoMapper)
    {
        _generalPersist = generalPersist;
        _lotPersist = lotPersist;
        _autoMapper = autoMapper;
    }

    public async Task AddLot(int eventId, BatchDto dto)
    {
        var lot = _autoMapper.Map<Batch>(dto);
        lot.EventId = eventId;
        _generalPersist.Add(lot);
        await _generalPersist.SaveChangesAsync();
    }

    public async Task<BatchDto[]> Put(int eventId, BatchDto[] models)
    {
        var lotes = _lotPersist.GetLotsByEventId(eventId);
        var result = lotes.Result;
        if (result == null) return null;

        foreach (var model in models)
        {
            model.EventId = eventId;
            if (model.Id == 0)
            {
                await AddLot(eventId, model);
            }
            else
            {
                await UpdateLot( lotes, model);
            }
        }

        var lotDto = await _lotPersist.GetLotsByEventId(eventId);
        return _autoMapper.Map<BatchDto[]>(lotDto);
    }

    private async Task UpdateLot( Task<Batch[]> lotes, BatchDto model)
    {
        var lote = lotes.Result.FirstOrDefault(l => l.Id == model.Id);
        _autoMapper.Map(model, lote);
        _generalPersist.Update(lote);
        await _generalPersist.SaveChangesAsync();
    }

    public async Task<bool> Delete(int eventoId, int lotId)
    {
        var batch = await _lotPersist.GetLotByIdsAsync(eventoId, lotId);
        if (batch == null) throw new Exception($"Doesnt exist a lot with id {eventoId} in event with id = {lotId}");
        _generalPersist.Delete(batch);
        return await _generalPersist.SaveChangesAsync();
    }

    public async Task<BatchDto[]> GetLotsByEventIdAsync(int eventId)
    {
        var lots = _lotPersist.GetLotsByEventId(eventId).Result;
        return lots == null ? null : _autoMapper.Map<BatchDto[]>(lots);
    }


    public async Task<BatchDto> GetLotByIdsAsync(int eventId, int lotId)
    {
        var lot = await _lotPersist.GetLotByIdsAsync(eventId, lotId);
        if (lot == null) return null;
        return _autoMapper.Map<BatchDto>(lot);
    }
}