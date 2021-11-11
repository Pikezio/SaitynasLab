using AutoMapper;
using SaitynasLab.Data.Dtos;
using SaitynasLab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<Concert, ConcertDto>();
            CreateMap<ConcertDto, Concert>();
            CreateMap<CreateConcertDto, Concert>();
            CreateMap<UpdateConcertDto, Concert>();
        }
    }

    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<SongDto, Song>();
            CreateMap<Song, SongDto>();
            CreateMap<CreateSongDto, Song>();
            CreateMap<UpdateSongDto, Song>();
        }
    }

    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<PartDto, Part>();
            CreateMap<Part, PartDto>();
            CreateMap<CreatePartDto, Part>();
            CreateMap<UpdatePartDto, Part>();
        }
    }
}
