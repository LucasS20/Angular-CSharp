namespace Proeventos.Models;

public class Evento 
{
    public int EventoId { get; set; }
    public string Local { get; set; }
    public string Data { get; set; }
    public string Theme { get; set; }
    public int NumberOfPeoples { get; set; }
    public string Lote { get; set; }
    public string imgURL { get; set; }
}