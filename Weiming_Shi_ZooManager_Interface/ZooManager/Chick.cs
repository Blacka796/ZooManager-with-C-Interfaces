using System;

namespace ZooManager
{
    public class Chick : Bird, Prey
    {
        public Chick(string name):base(name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name;
            reactionTime = new Random().Next(6, 11); // reaction time 6 (fast) to 10 (medium)
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick.");
            Flee("cat");
        }

    }
}

