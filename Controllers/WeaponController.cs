using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Dtos.WeaponDto;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controller")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }


        [HttpPost("AddWeapon")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(WeaponDto weapon)
        {
            var response = await _weaponService.AddWeapon(weapon);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
            
        }
    }
}
