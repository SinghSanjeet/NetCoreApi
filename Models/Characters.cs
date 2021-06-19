﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models
{
    public class Characters
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "Fredo";
        public int HitPoints { get; set; } = 10;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User User { get; set; }
        public Weapon Weapon { get; set; }
    }
}
