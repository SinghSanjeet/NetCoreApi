using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Dtos.WeaponDto;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Interfaces
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(WeaponDto weaponDto);
    }
}
