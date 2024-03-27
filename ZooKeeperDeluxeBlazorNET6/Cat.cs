using System;

namespace ZooKeeperDeluxe
{
    public class Cat : Animal, IPredator, IPrey
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)Cat
            remainingTime = 9 - reactionTime; //Cat has 9 times in total without eating before dying
        }

        public override void Activate()
        {
            base.Activate(); // First call the Activate method from our ancestor class...
                             // Console.WriteLine("I am a cat. Meow.");
            if (!(this as IPrey).Flee(this, location.x, location.y, "raptor"))
            {
                (this as IPredator).Hunt(this, location.x, location.y, "mouse");
                (this as IPredator).Hunt(this, location.x, location.y, "chick");
                SpeedUp();
            }
            else
            {
                SlowDown();
            }
            remainingTime = 9-reactionTime; //update remainingTime
        }
    }
}

