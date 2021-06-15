using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data;
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
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService( IMapper mapper, DataContext dataContext)
        {
            _context = dataContext;        
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            if(newCharacter != null)
            {
                Characters character = _mapper.Map<Characters>(newCharacter);                                
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

            }
            var dataContext = await _context.Characters.ToListAsync();
            response.Data =  _mapper.Map<List<GetCharacterDto>>(dataContext);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Characters character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                var dataContext = await _context.Characters.ToListAsync();
                response.Data = _mapper.Map <List<GetCharacterDto>>(dataContext);

                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllcharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            response.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dataContext = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(dataContext);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Characters characterToUpdate = await _context.Characters.FirstOrDefaultAsync(x => x.Id == character.Id);
                characterToUpdate.Name = character.Name;
                characterToUpdate.Strength = character.Strength;
                characterToUpdate.Intelligence = character.Intelligence;
                characterToUpdate.HitPoints = character.HitPoints;
                characterToUpdate.Class = character.Class;
                _context.Characters.Update(characterToUpdate);
                _context.SaveChanges();
                response.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.Message;
            }
           

            return response;
        }
    }
}
