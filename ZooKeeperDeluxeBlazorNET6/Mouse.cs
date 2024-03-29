using System;
namespace ZooKeeperDeluxe
{
    public class Mouse : Animal, IPrey
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            remainingTime = 6 - reactionTime; //Mouse has 6 times in total without eating before dying
            ReproductionInterval = 3;
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
             * so mice are more likely to react to their surroundings faster than cats!
             Mouse*/
        }


        public override void Activate()
        {
            base.Activate();
            if (!Graze()) // Changed method name to clarify this is not a hunter
            {
                if ((this as IPrey).Flee(this, location.x, location.y, "cat"))
                {
                    Console.WriteLine("I'm fleeing here!");
                    SlowDown();
                    SlowDown();
                } else
                {
                    Console.WriteLine("Phew! No need to run!");
                    SlowDown();
                }
            } else
            {
                Console.WriteLine("Zoomy zoom zoom!");
                SpeedUp();
            }
            remainingTime = 6 - reactionTime; //update remainingTime
            existingTime++;
        }


        /* Updated to check if grazing was successful */
        public bool Graze()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "grass"))
            {
                Game.Attack(this, Direction.up);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "grass"))
            {
                Game.Attack(this, Direction.down);
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.left, "grass"))
            {
                Game.Attack(this, Direction.left);
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.right, "grass"))
            {
                Game.Attack(this, Direction.right);
                return true;
            }
            return false;
        }
    }
}

