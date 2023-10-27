using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ILotPersist
{/// <summary>
 ///    Return a array of Lots by the event Id
 /// </summary>
 /// <param name="eventoId"></param>
 /// <returns></returns>
    Task<Lot[]> GetLotsByEventId(int eventoId);
 
    /// <summary>
    ///     Return a lot from the database
    /// </summary>
    /// <param name="eventoId">eventID from the event where the lot is</param>
    /// <param name="lotId">Lot ID</param>
    /// <returns></returns>
    Task<Lot> GetLotByIdsAsync(int eventoId, int lotId);
}