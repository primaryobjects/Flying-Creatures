using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingCreatures.Repository.Interface;
using FlyingCreatures.Models;
using StructureMap;

namespace FlyingCreatures.Managers
{
    public class CreatureManager
    {
        #region Names

        private static string[] creatureFirstName = new string[10]
        {
            "White",
            "Black",
            "Light",
            "Dark",
            "Evil",
            "Cunning",
            "Magic",
            "Silver",
            "Golden",
            "Slimy"
        };

        private static string[] creatureLastName = new string[10]
        {
            "Gargoyle",
            "Dragon",
            "Wraith",
            "Harpie",
            "Nymph",
            "Serpent",
            "Bat",
            "Chimaera",
            "Hippogryph",
            "Spirit"
        };

        #endregion

        private static IRepository<Creature> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Creature>>();
            }
        }

        public static Creature Load(string id)
        {
            return _repository.Single(u => u.Id == id);
        }

        public static List<Creature> GetAll()
        {
            List<Creature> list = new List<Creature>();

            // Fetch all entities from the repository.
            list = _repository.GetAll().ToList();

            return list;
        }

        public static void Insert(Creature entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);
        }

        public static void Delete(Creature entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }

        public static Creature CreateRandom()
        {
            Creature creature = new Creature();

            Random random = new Random((int)DateTime.Now.Ticks);

            creature.Name = HelperManager.CreateRandomName(creatureFirstName, creatureLastName);
            creature.Age = random.Next(1, 500);
            creature.Weapon = WeaponManager.CreateRandom();

            return creature;
        }
    }
}
