using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Dtos.WeaponDto;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreApi.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUser() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(WeaponDto weaponDto)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.
                                    FirstOrDefaultAsync(x => x.Id == weaponDto.CharacterId &&
                                    x.User.Id == GetUser());
                if (character == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Character not found.";
                    return response;
                }

                Weapon weapon = _mapper.Map<Weapon>(weaponDto);
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);

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
