using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PestTracking.Models;
using PestTracking.Models.Dtos;

namespace PestTracking.PestTrackingMappers
{
    public class PestsTrackingMapper : Profile
    {
        public PestsTrackingMapper()
        {
            //Maps de paises
            CreateMap<Pais,PaisDto>().ReverseMap();
            CreateMap<Pais,CrearPaisDto>().ReverseMap();

            //Maps de empresas
            CreateMap<Empresa,EmpresaDto>().ReverseMap();
            CreateMap<Empresa,CrearEmpresaDto>().ReverseMap();

            //Map de caracteristicas
            CreateMap<Caracteristica, CaracteristicaDto>().ReverseMap();
            CreateMap<Caracteristica, CrearCaracteristicaDto>().ReverseMap();
        }
    }
}