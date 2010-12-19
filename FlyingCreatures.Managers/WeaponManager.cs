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

        public static void Create(Weapon entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);
        }

        public static void Delete(Weapon entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }
    }
}
