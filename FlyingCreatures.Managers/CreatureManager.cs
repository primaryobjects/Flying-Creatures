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

        public static void Create(Creature entity)
        {
            // Add the new entity to the repository.
            _repository.Add(entity);
        }

        public static void Delete(Creature entity)
        {
            // Add any custom business rules.
            _repository.Delete(entity);
        }
    }
}
