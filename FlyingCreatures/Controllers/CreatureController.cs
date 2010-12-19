using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyingCreatures.Repository.Concrete;
using FlyingCreatures.Managers;
using FlyingCreatures.Models;

namespace FlyingCreatures.Controllers
{
    public class CreatureController : Controller
    {
        private Random _random = new Random((int)DateTime.Now.Ticks);

        public ActionResult Index()
        {
            return View(CreatureManager.GetAll());
        }

        [HttpPost]
        public ActionResult GenerateCreature()
        {
            Weapon weapon = new Weapon()
            {
                Name = "Claws",
                Type = Weapon.WeaponType.Melee,
                DamageMinimum = 0,
                DamageMaximum = 3
            };

            Creature creature = new Creature
            {
                Name = "Random" + _random.Next(1, 500),
                Age = _random.Next(1, 500),
                Weapon = weapon
            };

            CreatureManager.Create(creature);

            UnitOfWork.Commit();

            return PartialView("_CreatureGrid", CreatureManager.GetAll());
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            Creature creature = CreatureManager.Load(id);
            CreatureManager.Delete(creature);

            UnitOfWork.Commit();

            return PartialView("_CreatureGrid", CreatureManager.GetAll());
        }


        /*
        //
        // This method can be used to only return the newly added Row as html to append to the table. Use this by replacing the existing Ajax Form for GenerateCreature in Index.cshtml with:
        // @using (@Ajax.BeginForm("GenerateCreature2", "Index", new AjaxOptions { LoadingElementId = "ctrlProgress", InsertionMode = InsertionMode.InsertAfter, UpdateTargetId = "creatureGridBody", OnSuccess = "successLoad();" }, new { id = "addLink" })) { }
        //
        [HttpPost]
        public string GenerateCreature2()
        {
            Weapon weapon = new Weapon()
            {
                Name = "Claws",
                Type = Weapon.WeaponType.Melee,
                DamageMinimum = 0,
                DamageMaximum = 3
            };

            Creature creature = new Creature
            {
                Name = "Random" + _random.Next(1, 500),
                Age = _random.Next(1, 500),
                Weapon = weapon
            };

            CreatureManager.Create(creature);

            UnitOfWork.Commit();

            string result = "<tr><td>" + creature.Name + "</td><td>" + creature.Age + "</td><td>" + creature.Weapon.Name + " (" + creature.Weapon.DamageMinimum + " - " + creature.Weapon.DamageMaximum + " dmg)</td></tr>";
            return result;
        }*/
    }
}
