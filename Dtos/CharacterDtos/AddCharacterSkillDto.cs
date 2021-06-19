using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Dtos.CharacterDtos
{
    public class AddCharacterSkillDto
    {
        public int CharacterID { get; set; }
        public int SkillId { get; set; }
    }
}
