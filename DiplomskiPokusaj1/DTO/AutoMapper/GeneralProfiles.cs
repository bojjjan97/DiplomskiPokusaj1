using AutoMapper;
using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.DTO.View;
using DiplomskiPokusaj1.DTO.Create;
using DiplomskiPokusaj1.DTO.Update;

namespace MassiveDynamicApi.DTO.Automapper
{
    public class GeneralProfiles : Profile
    {
        public GeneralProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, PublicUserDTO>().ReverseMap();

            // CreateMap<Category, CategoryDTO>()
            //   .ForMember(mat => mat.Materials, opt => opt.MapFrom(src => src.Materials.Select(c => c.Id)))
            // .ReverseMap();

            CreateMap<Genre, ViewGenreDTO>()
                .ReverseMap();
            CreateMap<Genre, CreateGenreDTO>()
                .ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>()
                .ReverseMap();

        }
    }
}
