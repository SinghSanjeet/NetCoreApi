using AutoMapper;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Dtos.WeaponDto;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Characters, GetCharacterDto>();
            CreateMap<AddCharacterDto, Characters>();
            CreateMap<AddCharacterDto, UpdateCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<WeaponDto, Weapon>();
        }
    }
}
