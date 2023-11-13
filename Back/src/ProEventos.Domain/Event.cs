﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain;

[Table("Events")]
public class Event
{
    [Key] public int Id { get; set; }
    public string Local { get; set; }
    public DateTime Date { get; set; }
    [Required] public string Theme { get; set; }
    public int NumberOfPeoples { get; set; }
    public string ImgUrl { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<Batch> Lots { get; set; }
    public IEnumerable<SocialMedia> SocialMedias { get; set; }
    public Speaker Speaker { get; set; }
}