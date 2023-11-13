namespace ProEventos.Application.Dtos;

public class SpeakerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Resume { get; set; }
    public string ImageUrl { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<SocialMediaDto> SocialMedias { get; set; }
    public IEnumerable<EventDto> Events { get; set; }
}