using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ILotService
{

    Task<LotDto[]> Put( int eventId,LotDto[] models);
    Task<bool> Delete(int eventoId,int lotId);
    Task<LotDto[]> GetLotsByEventIdAsync(int eventId);
    Task<LotDto> GetLotByIdsAsync(int eventoId, int lotId);
}