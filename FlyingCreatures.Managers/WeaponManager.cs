using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingCreatures.Repository.Interface;
using FlyingCreatures.Models;
using StructureMap;

namespace FlyingCreatures.Managers
{
    public class WeaponManager
    {
        #region Names

        private static string[] weaponFirstName = new string[10]
        {
            "Magic",
            "Poison",
            "Fire",
            "Acid",
            "Strong",
            "Sharp",
            "Blazing",
            "Silver",
            "Golden",
            "Bronze"
        };

        private static string[] weaponLastName = new string[10]
        {
            "Claws",
            "Teeth",
            "Arrow",
            "Dagger",
            "Sword",
            "Bolt",
            "Lasso",
            "Axe",
            "Harpoon",
            "Scimitar"
        };

        #endregion

        private static IRepository<Weapon> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Weapon>>();
            }
        }

        public static Weapon Load(string id)
        {
            return _repository.Single(u => u.Id == id);
        }

        public static List<Weapon> GetAll()
        {
            List<Weapon> list = new List<Weapon>();

            // Fetch all entities from the repository.
            list = _repository.GetAll().ToList();

            return list;
        }

        public static void Insert(Weapon entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);
        }

        public static void Delete(Weapon entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }

        public static Weapon CreateRandom()
        {
            Weapon weapon = new Weapon();

            Random random = new Random((int)DateTime.Now.Ticks);

            weapon.Name = HelperManager.CreateRandomName(weaponFirstName, weaponLastName);
            weapon.Type = Weapon.WeaponType.Melee;
            weapon.DamageMinimum = random.Next(3);
            weapon.DamageMaximum = random.Next(4, 15);

            return weapon;
        }
    }
}
