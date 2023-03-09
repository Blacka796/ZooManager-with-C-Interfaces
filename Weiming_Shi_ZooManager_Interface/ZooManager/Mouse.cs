using System;
namespace ZooManager
{
    public class Mouse : Animal, Prey
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
             * so mice are more likely to react to their surroundings faster than cats!
             */
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            if (Flee("cat"))
            {
                return;
            }
            if (Flee("raptor"))
            {
                return;
            }
        }

        /* Note that our mouse is (so far) a teeny bit more strategic than our cat.
         * The mouse looks for cats and tries to run in the opposite direction to
         * an empty spot, but if it finds that it can't go that way, it looks around
         * some more. However, the mouse currently still has a major weakness! He
         * will ONLY run in the OPPOSITE direction from a cat! The mouse won't (yet)
         * consider running to the side to escape! However, we have laid out a better
         * foundation here for intelligence, since we actually check whether our escape
         * was succcesful -- unlike our cats, who just assume they'll get their prey!
         */
        public override bool Flee(string animal)
        {
            /*
            if (Game.Seek(location.x, location.y, Direction.up, "cat"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "cat"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "cat"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "cat"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
            */

            /*
             * g/ if detects a predator nearby, it 
            attempts to move 2 squares in a random direc?on instead of using Retreat.
            */
            bool predatorNearby = false;
            if (Game.Seek(location.x, location.y, Direction.up, animal))
            {
                predatorNearby = true;
            }
            if (Game.Seek(location.x, location.y, Direction.down, animal))
            {
                predatorNearby = true;
            }
            if (Game.Seek(location.x, location.y, Direction.left, animal))
            {
                predatorNearby = true;
            }
            if (Game.Seek(location.x, location.y, Direction.right, animal))
            {
                predatorNearby = true;
            }

            if (predatorNearby) {
                Direction[] directions = new Direction[] { Direction.down, Direction.up, Direction.left, Direction.right };
                int index = new Random().Next(4);
                Direction d = directions[index];

                int totalMove = 0;
                int moved = this.Move(d, 2);
                totalMove += moved;

                /* h/ */
                /*
                 * if steps actully moved is less than 2, call Move again
                 */
                if (moved < 2)
                {
                    if(d.Equals(Direction.up) || d.Equals(Direction.down))
                    {
                        directions = new Direction[] { Direction.left, Direction.right };
                    }
                    else
                    {
                        directions = new Direction[] { Direction.down, Direction.up};
                    }
                    
                    index = new Random().Next(4);
                    d = directions[index];
                    totalMove += this.Move(d, 2 - moved);
                }

                return totalMove > 0;
            }
            else
            {
                return false;
            }
        }
    }
}

