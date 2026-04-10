using AutoMapper;
using MinhasFinancas.Common.DTOs;
using MinhasFinancas.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinhasFinancas.API.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>().ReverseMap();
            CreateMap<CategoriaRequestDto, Categoria>().ReverseMap();

            CreateMap<Transacao, TransacaoResponseDto>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : string.Empty))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.NomeCompleto : string.Empty));


            CreateMap<TransacaoRequestDto, Transacao>()
                .ForMember(dest => dest.TransacaoId, opt => opt.Ignore())
                .ForMember(dest => dest.Categoria, opt => opt.Ignore())
                .ForMember(dest => dest.Usuario, opt => opt.Ignore());
        }
    }
}
