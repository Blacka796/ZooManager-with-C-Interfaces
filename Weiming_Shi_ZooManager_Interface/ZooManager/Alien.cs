using System;

namespace ZooManager
{
    public class Alien : Predator
    {
        public Animal animal;

        public Alien(string name)
        {
            animal = new Animal();

            animal.emoji = "👽";
            animal.species = "alien";
            animal.name = name;
            animal.reactionTime = 1; // reaction time 1 
            animal.predator = this;
        }

        public void Activate()
        {
            Console.WriteLine("I am a alien.");
            Hunt("");
        }
 
        public bool Hunt(string a)
        {
            if (animal.Hunt("cat"))
            {
                return true;
            }

            if (animal.Hunt("mouse"))
            {
                return true;
            }

            if (animal.Hunt("raptor"))
            {
                return true;
            }

            if (animal.Hunt("chick"))
            {
                return true;
            }
            return false;
        }
    }     
}

