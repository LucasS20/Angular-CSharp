using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class SpeakerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Resume { get; set; }
    public string ImageURL { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public IEnumerable<SocialMediaDto> SocialMedias { get; set; }
    public IEnumerable<SpeakerDto> EventSpeakers { get; set; }
}