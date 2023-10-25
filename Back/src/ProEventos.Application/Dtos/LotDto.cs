namespace ProEventos.Application.Dtos;

public class LotDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int Quantidade { get; set; }
    public int EventId { get; set; }
    // public EventDto Event { get; set; }
}