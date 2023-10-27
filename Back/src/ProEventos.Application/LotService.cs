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

    public async Task AddLot(int eventId, LotDto dto)
    {
        var lot = _autoMapper.Map<Lot>(dto);
        lot.EventId = eventId;
        _generalPersist.Add(lot);
        await _generalPersist.SaveChangesAsync();
    }

    public async Task<LotDto[]> SaveLot(int eventId, LotDto[] models)
    {
        var lotes = _lotPersist.GetLotsByEventId(eventId);
        if (lotes == null) return null;
        foreach (var model in models)
        {
            if (model.Id != 0)
            {
                await AddLot(eventId, model);
            }
            else
            {
                await UpdateLot(eventId, lotes, model);
            }
        }

        var lotDto = await _lotPersist.GetLotsByEventId(eventId);
        return _autoMapper.Map<LotDto[]>(lotDto);
    }

    private async Task UpdateLot(int eventId, Task<Lot[]> lotes, LotDto model)
    {
        var lote = lotes.Result.FirstOrDefault(l => l.Id == model.Id);
        _autoMapper.Map(model, lote);
        model.EventId = eventId;
        _generalPersist.Update(lote);
        await _generalPersist.SaveChangesAsync();
    }

    public async Task<bool> Delete(int eventId, int lotId)
    {
        var _event = await _lotPersist.GetLotByIdsAsync(eventId, lotId);
        if (_event == null) throw new Exception($"Doesnt exist a lot with id {lotId} in event with id = {eventId}");
        _generalPersist.Delete(_event);
        return await _generalPersist.SaveChangesAsync();
    }

    public async Task<LotDto[]> GetLotsByEventIdAsync(int eventId)
    {
        var lots = _lotPersist.GetLotsByEventId(eventId);
        return await lots == null ? null : _autoMapper.Map<LotDto[]>(lots);
    }


    public async Task<LotDto> GetLotByIdsAsync(int eventId, int lotId)
    {
        var lot = await _lotPersist.GetLotByIdsAsync(eventId, lotId);
        if (lot == null) return null;
        return _autoMapper.Map<LotDto>(lot);
    }
}