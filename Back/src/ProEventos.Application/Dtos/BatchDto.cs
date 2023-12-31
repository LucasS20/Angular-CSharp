﻿namespace ProEventos.Application.Dtos;

public class BatchDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TicketAmount { get; set; }
    public int EventId { get; set; }
    // public EventDto Event { get; set; }
}