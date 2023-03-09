using System;

namespace ZooManager
{
    public class Bird : Animal
    {
        public Bird(string name)
        {
            emoji = "🐦";
            species = "bird";
            this.name = name;
            reactionTime = 1; // reaction time 1 
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a bird.");
            Hunt();
        }
    }
}

