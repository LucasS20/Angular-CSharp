using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ILotService
{

    Task<LotDto[]> SaveLot( int eventId,LotDto[] models);
    Task<bool> Delete(int lotId,int eventoId);
    Task<LotDto[]> GetLotsByEventIdAsync(int eventId);
    Task<LotDto> GetLotByIdsAsync(int eventoId, int lotId);
}