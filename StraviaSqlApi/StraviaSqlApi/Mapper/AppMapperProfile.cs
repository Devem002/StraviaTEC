using AutoMapper;
using StraviaSqlApi.Dtos;
using StraviaSqlApi.Entities;
using ActReto = StraviaSqlApi.Entities.ActReto;

namespace StraviaSqlApi.Mapper;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<ActCarreraDto, ActCarrera>();
        CreateMap<ActRetoDto, ActReto>();
        CreateMap<ActividadDto, Actividad>();
        CreateMap<AmigosDto, Amigos>();
        CreateMap<AtletaDto, Atleta>();
        CreateMap<CarreraDto, Carrera>();
        CreateMap<CategoriaDto, Categoria>();
        CreateMap<ClasificacionActividadDto, ClasificacionActividad>();
        CreateMap<CuentaBancoDto, CuentaBanco>();
        CreateMap<FondoAlturaDto, FondoAltura>();
        CreateMap<GruposDto, Grupos>();
        CreateMap<InscritoDto, Inscrito>();
        CreateMap<IntegranteDto, Integrantes>();
        CreateMap<NacionalidadDto, Nacionalidad>();
        CreateMap<PatrocinaCarreraDto, PatrocinaCarrera>();
        CreateMap<PatrocinadorDto, Patrocinador>();
        CreateMap<PatrocinaRetoDto, PatrocinaReto>();
        CreateMap<PrivCarreraDto, PrivCarrera>();
        CreateMap<PrivRetoDto, PrivReto>();
        CreateMap<RetosDto, Retos>();
    }
    
}