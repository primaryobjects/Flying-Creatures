using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingCreatures.Models
{
    public class Creature
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Weapon Weapon { get; set; }
    }
}
