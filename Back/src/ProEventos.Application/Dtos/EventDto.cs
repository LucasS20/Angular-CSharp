using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos;

public class EventDto
{
    public int Id { get; set; }
    public string Local { get; set; }
    public string Date { get; set; }

    [Required(ErrorMessage = "The {0} is required"),
    StringLength(50,MinimumLength = 3,ErrorMessage = "Length should be in the range 3-50")
    ]
    public string Theme { get; set; }
    [Range(1,120000,ErrorMessage =  "Length should be in the range 3-50")]
    public int NumberOfPeoples { get; set; }
    public string ImgUrl { get; set; }
    [Required(ErrorMessage = "The {0} is requireD"),Phone(ErrorMessage = "The {0} has a invalid format")]
    public string Phone { get; set; }
    [EmailAddress(ErrorMessage = "The email must be a valid email adress"),Required(ErrorMessage = "The {0} is required")]
    public string Email { get; set; }

    public IEnumerable<LotDto> Lots { get; set; }
    public IEnumerable<SocialMediaDto> SocialMedias { get; set; }
    public IEnumerable<SpeakerDto> SpeakersEvent { get; set; }
}