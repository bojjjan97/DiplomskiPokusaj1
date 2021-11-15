﻿using AutoMapper;
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

            CreateMap<Genre, ViewGenreDTO>()
                .ReverseMap();
            CreateMap<Genre, CreateGenreDTO>()
                .ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>()
                .ReverseMap();

            CreateMap<Category, CreateCategoryDTO>()
                .ReverseMap();
            CreateMap<Category, ViewCategoryDTO>()
                .ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>()
                .ReverseMap();

            CreateMap<Author, CreateAuthorDTO>()
                .ReverseMap();
            CreateMap<Author, ViewAuthorDTO>()
                .ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>()
                .ReverseMap();


            CreateMap<Address, CreateAddressDTO>()
                .ReverseMap();
            CreateMap<Address, ViewAddressDTO>()
                .ReverseMap();
            CreateMap<Address, UpdateAddressDTO>()
                .ReverseMap();

            CreateMap<Publisher, CreatePublisherDTO>()
                .ReverseMap();
            CreateMap<Publisher, ViewPublisherDTO>()
                .ReverseMap();
            CreateMap<Publisher, UpdatePublisherDTO>()
                .ReverseMap();

            CreateMap<Material, CreateMaterialDTO>()
                .ReverseMap();
            CreateMap<Material, ViewMaterialDTO>()
                .ReverseMap();
            CreateMap<Material, UpdateMaterialDTO>()
                .ReverseMap();

            CreateMap<MaterialCopy, ViewMaterialCopyDTO>()
                .ReverseMap();

        }
    }
}
