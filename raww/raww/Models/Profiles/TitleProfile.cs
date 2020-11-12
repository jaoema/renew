using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;
using AutoMapper;

namespace raww.Models.Profiles
{
    public class TitleProfile :  Profile
    {
        public TitleProfile()
        {
            CreateMap<Titlebasics, TitleDto>().ReverseMap();
        }
    }
}
