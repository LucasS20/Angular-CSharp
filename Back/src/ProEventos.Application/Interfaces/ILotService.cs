using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ILotService
{

    Task<BatchDto[]> Put( int eventId,BatchDto[] models);
    Task<bool> Delete(int eventoId,int lotId);
    Task<BatchDto[]> GetLotsByEventIdAsync(int eventId);
    Task<BatchDto> GetLotByIdsAsync(int eventoId, int lotId);
}