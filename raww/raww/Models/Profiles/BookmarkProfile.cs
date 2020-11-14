using AutoMapper;
using DataserviceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Models.Profiles
{
    public class BookmarkProfile : Profile
    {
        public BookmarkProfile()
        {
            CreateMap<Bookmark, BookmarkDto>();
        }
    }
}
