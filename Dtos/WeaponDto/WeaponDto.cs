using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Dtos.WeaponDto
{
    public class WeaponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
