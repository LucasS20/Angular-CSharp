﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Domain;

public class Lot
{
     public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quantidade { get; set; }
    public int EventId { get; set; }
}