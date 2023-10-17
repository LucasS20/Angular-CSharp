namespace ProEventos.Domain;

public class Speaker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Resume { get; set; }
    public string ImageURL { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public IEnumerable<SocialMedia> SocialMedias { get; set; }
    public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
}