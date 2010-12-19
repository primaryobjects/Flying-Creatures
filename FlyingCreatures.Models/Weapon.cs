using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingCreatures.Models
{
    public class Weapon
    {
        public enum WeaponType
        {
            Melee,
            Ranged,
            Magic
        };

        public string Id { get; set; }
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public int DamageMinimum { get; set; }
        public int DamageMaximum { get; set; }
    }
}
