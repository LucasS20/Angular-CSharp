using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers;

public class ProEventosProfile : Profile
{
    public ProEventosProfile()
    {
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Lot, LotDto>().ReverseMap();
        CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();
        CreateMap<Speaker, SpeakerDto>();
    }
}