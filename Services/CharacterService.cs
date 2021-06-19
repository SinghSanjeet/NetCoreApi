using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreApi.Services
{
    public class CharacterService : ICharacterService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService( IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = dataContext;        
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto characterDto)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                if (characterDto != null)
                {
                    Characters character = _mapper.Map<Characters>(characterDto);
                    character.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
                    _context.Characters.Add(character);
                    await _context.SaveChangesAsync();

                }
                var dataContext = await _context.Characters.ToListAsync();
                response.Data = _mapper.Map<List<GetCharacterDto>>(dataContext);
            }
            catch (Exception ex)
            {

               response.IsSuccess = false;
                response.Message = ex.Message;
            }
           
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Characters character = await _context.Characters.Include(x=>x.User).FirstOrDefaultAsync
                                                            (x => x.Id == id && x.User.Id == GetUserId());
                if(character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    var dataContext = await _context.Characters.Where(x => x.Id == GetUserId()).ToListAsync();
                    response.Data = _mapper.Map<List<GetCharacterDto>>(dataContext);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Character not found.";
                }
                

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
            var dbCharacters = await _context.Characters.Where( x => x.User.Id == GetUserId()).ToListAsync();
            response.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dataContext = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
            response.Data = _mapper.Map<GetCharacterDto>(dataContext);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Characters characterToUpdate = await _context.Characters.Include(c=>c.User).FirstOrDefaultAsync
                                                            (x => x.Id == character.Id && x.User.Id == GetUserId());
                if(characterToUpdate != null)
                {
                    characterToUpdate.Name = character.Name;
                    characterToUpdate.Strength = character.Strength;
                    characterToUpdate.Intelligence = character.Intelligence;
                    characterToUpdate.HitPoints = character.HitPoints;
                    characterToUpdate.Class = character.Class;
                    _context.Characters.Update(characterToUpdate);
                    _context.SaveChanges();
                    response.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
                }else
                {
                    response.IsSuccess = false;
                    response.Message = "Character does not exists.";
                }
                
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
