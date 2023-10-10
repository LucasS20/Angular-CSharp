namespace ProEventos.Domain;

public class Event
{
    public int Id { get; set; }
    public string Local { get; set; }
    public DateTime Date { get; set; }
    public string Theme { get; set; }
    public int NumberOfPeoples { get; set; }
    public string imgURL { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public IEnumerable<Lot> Lots { get; set; }
    public IEnumerable<SocialMedia> SocialMedias { get; set; }
    public IEnumerable<EventSpeaker> SpeakersEvent { get; set; }
}