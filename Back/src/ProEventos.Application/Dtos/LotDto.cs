using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class LotDto
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
    string StartDate { get; set; }
    string EndDate { get; set; }
    int Quantidade { get; set; }
    int EventId { get; set; }
    EventDto Event { get; set; }
}