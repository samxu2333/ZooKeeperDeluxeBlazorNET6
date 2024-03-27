using System;
namespace ZooKeeperDeluxe
{
    public class Animal : Occupant
    {
        public string name;
        public int reactionTime = 5; // default reaction time for animals (for this game, range is 1 - 9, with 10 being dead)
        public bool acted = false; // has the animal acted yet this turn?
        public int remainingTime = 0; //Times remaning for animals before death.

        /* "virtual" indicates that this method can be overridden by a method of the same name in classes descended from Animal */
        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }

        public void SlowDown()
        {
            reactionTime++;
            if (reactionTime > 10)
            {
                reactionTime = 10;
            }
            Console.WriteLine($"Animal {name} slowed to {reactionTime}");
        }

        public void SpeedUp()
        {
            reactionTime--;
            if (reactionTime < 1)
            {
                reactionTime = 1;
            }
        }
    }
}
