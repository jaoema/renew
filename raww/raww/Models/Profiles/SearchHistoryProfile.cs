﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;

namespace raww.Models.Profiles
{
    public class SearchHistoryProfile : Profile
    {
        SearchHistoryProfile()
        {
            CreateMap<Searchhistory, SearchHistoryDto>();
        }
    }
}
