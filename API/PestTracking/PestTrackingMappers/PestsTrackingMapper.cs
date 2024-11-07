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
            CreateMap<Pais,PaisDto>().ReverseMap();
            CreateMap<Pais,CrearPaisDto>().ReverseMap();
        }
    }
}