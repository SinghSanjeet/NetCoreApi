using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Dtos.CharacterDtos;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase 
    {
        private readonly ICharacterService _charaterService;

        public CharacterController(ICharacterService characterService)
        {
            _charaterService = characterService;
        }

        //route
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _charaterService.GetAllcharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _charaterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            
            return Ok(await _charaterService.AddNewCharacter(newCharacter));
        }
        

    }
}
