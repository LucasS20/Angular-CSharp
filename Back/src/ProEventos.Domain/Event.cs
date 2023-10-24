using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ProEventos.Domain;

[Table("Events")]
public class Event
{
    [Key] public int Id { get; set; }
    public string Local { get; set; }
    public DateTime Date { get; set; }
    [Required] public string Theme { get; set; }
    [NotMapped] public int Dias { get; set; }
    public int NumberOfPeoples { get; set; }
    public string ImgUrl { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<Lot> Lots { get; set; }
    public IEnumerable<SocialMedia> SocialMedias { get; set; }
    public IEnumerable<EventSpeaker> SpeakersEvent { get; set; }
}