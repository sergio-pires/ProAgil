using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>().ForMember(
                dest => dest.Oradores, opt => {
                    opt.MapFrom(src => src.OradoresEventos.Select( x => x.Orador).ToList());
                }
            )
            .ReverseMap();
            
            CreateMap<Orador, OradorDto>().ForMember(
                dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.OradoresEventos.Select(x => x.Evento).ToList());
                }
            )
            .ReverseMap();

            CreateMap<Lote, LoteDto>()
            .ReverseMap();

            CreateMap<RedeSocial, RedeSocialDto>()
            .ReverseMap();
        }
    }
}