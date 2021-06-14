﻿using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Interfaces
{
    public  interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto character);
    }
}