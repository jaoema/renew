using AutoMapper;
using DataserviceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Models.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, CoActorDto>();
            CreateMap<Person, PopularActorDto>();
        }
    }
}
