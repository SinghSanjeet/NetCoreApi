using AutoMapper;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly List<Characters> _characters;
        private readonly IMapper _mapper;

        public CharacterService( IMapper mapper)
        {
        _characters = new List<Characters>
        {
            new Characters{Id = 1},
            new Characters{Id = 2, Name = "Sanjeet"}
        };
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            if(newCharacter != null)
            {
                Characters character = _mapper.Map<Characters>(newCharacter);
                character.Id = _characters.Max(x => x.Id) + 1;
               _characters.Add(character);

            }
            response.Data = _mapper.Map<List<GetCharacterDto>>(_characters);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = _mapper.Map<List<GetCharacterDto>>(_characters);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            response.Data = _mapper.Map<GetCharacterDto>(_characters.FirstOrDefault(x => x.Id == id));
            return response;
        }
    }
}
