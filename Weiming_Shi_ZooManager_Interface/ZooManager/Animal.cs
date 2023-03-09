using System;
namespace ZooManager
{
    public class Animal
    {
        public string emoji;
        public string species;
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        
        public Point location;
        public Predator predator;

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }

        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
            if(predator != null)
            {
                predator.Activate();
            }
        }

        /* This method currently assumes that the attacker has determined there is prey
         * in the target direction. In addition to bug-proofing our program, can you think
         * of creative ways that NOT just assuming the attack is on the correct target (or
         * successful for that matter) could be used?
        */
        public void Attack(Direction d)
        {
            Animal attacker = this;
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    Game.animalZones[y - 1][x].occupant = attacker;
                    break;
                case Direction.down:
                    Game.animalZones[y + 1][x].occupant = attacker;
                    break;
                case Direction.left:
                    Game.animalZones[y][x - 1].occupant = attacker;
                    break;
                case Direction.right:
                    Game.animalZones[y][x + 1].occupant = attacker;
                    break;
            }
            Game.animalZones[y][x].occupant = null;
        }


        /* We can't make the same assumptions with this method that we do with Attack, since
         * the animal here runs AWAY from where they spotted their target (using the Seek method
         * to find a predator in this case). So, we need to figure out if the direction that the
         * retreating animal wants to move is valid. Is movement in that direction still on the board?
         * Is it just going to send them into another animal? With our cat & mouse setup, one is the
         * predator and the other is prey, but what happens when we have an animal who is both? The animal
         * would want to run away from their predators but towards their prey, right? Perhaps we can generalize
         * this code (and the Attack and Seek code) to help our animals strategize more...
         */

        public bool Retreat(Direction d)
        {
            Animal runner = this;
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    /* The logic below uses the "short circuit" property of Boolean &&.
                     * If we were to check our list using an out-of-range index, we would
                     * get an error, but since we first check if the direction that we're modifying is
                     * within the ranges of our lists, if that check is false, then the second half of
                     * the && is not evaluated, thus saving us from any exceptions being thrown.
                     */
                    if (y > 0 && Game.animalZones[y - 1][x].occupant == null)
                    {
                        Game.animalZones[y - 1][x].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                /* Note that in these four cases, in our conditional logic we check
                 * for the animal having one square between itself and the edge that it is
                 * trying to run to. For example,in the above case, we check that y is greater
                 * than 0, even though 0 is a valid spot on the list. This is because when moving
                 * up, the animal would need to go from row 1 to row 0. Attempting to go from row 0
                 * to row -1 would cause a runtime error. This is a slightly different way of testing
                 * if 
                 */
                case Direction.down:
                    if (y < Game.numCellsY - 1 && Game.animalZones[y + 1][x].occupant == null)
                    {
                        Game.animalZones[y + 1][x].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && Game.animalZones[y][x - 1].occupant == null)
                    {
                        Game.animalZones[y][x - 1].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < Game.numCellsX - 1 && Game.animalZones[y][x + 1].occupant == null)
                    {
                        Game.animalZones[y][x + 1].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }

        /**
        * g/ Move 
        */
        public int Move(Direction d, int distance)
        {
            Animal animal = this;
            int x0 = animal.location.x;
            int y0 = animal.location.y;

            int x = animal.location.x;
            int y = animal.location.y;
            int far = 0;
            for (int i = 1; i <= distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > Game.numCellsY - 1 || x > Game.numCellsX - 1)
                {
                    break;
                }
                if (Game.animalZones[y][x].occupant != null)
                {
                    break;
                }

                Game.animalZones[y][x].occupant = animal;
                Game.animalZones[y0][x0].occupant = null;
                x0 = x;
                y0 = y;
            }
            return far;
        }

        virtual public void Hunt()
        {

        }

        public bool Hunt(string animal)
        {
            if (Game.Seek(location.x, location.y, Direction.up, animal))
            {
               this.Attack(Direction.up);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, animal))
            {
               this.Attack(Direction.down);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, animal))
            {
               this.Attack(Direction.left);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, animal))
            {
               this.Attack(Direction.right);
                return true;
            }

            return false;
        }

        virtual public bool Flee(string animal)
        {
            if (Game.Seek(location.x, location.y, Direction.up, animal))
            {
                this.Retreat(Direction.down);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, animal))
            {
                this.Retreat(Direction.up);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, animal))
            {
                this.Retreat(Direction.right);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, animal))
            {
                this.Retreat(Direction.left);
                return true;
            }

            return false;
        }
    }
}
