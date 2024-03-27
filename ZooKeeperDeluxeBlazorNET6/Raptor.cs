namespace ZooKeeperDeluxe
{
    public class Raptor : Bird, IPredator
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast) to 5 (medium)Cat
            remainingTime = 10 - reactionTime;
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. Wow.");
            (this as IPredator).Hunt(this, location.x, location.y, "mouse");
            (this as IPredator).Hunt(this, location.x, location.y, "cat");
            remainingTime = 10 - reactionTime;
        }
    }
}
