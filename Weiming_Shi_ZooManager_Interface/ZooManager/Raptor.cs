using System;

namespace ZooManager
{
    public class Raptor : Bird, Predator
    {
        public Raptor(string name):base(name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 
        }
        
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. Meow.");
            Hunt();
        }

        /* 
         * Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */
        public override void Hunt()
        {
            if (Hunt("cat"))
            {
                return;
            }

            if (Hunt("mouse"))
            {
                return;
            }
        }
    }
}

