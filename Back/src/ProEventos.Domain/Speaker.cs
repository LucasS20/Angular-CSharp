namespace ProEventos.Domain;

public class Speaker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Resume { get; set; }
    public string ImageUrl { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<SocialMedia> SocialMedias { get; set; }
    public IEnumerable<Event> Events { get; set; }
}