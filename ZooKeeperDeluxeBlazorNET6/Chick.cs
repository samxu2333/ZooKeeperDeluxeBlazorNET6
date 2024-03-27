namespace ZooKeeperDeluxe
{
    public class Chick : Bird, IPrey
    {
        public Chick(string name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name;
            reactionTime = new Random().Next(6, 10); // reaction time 1 (fast) to 5 (medium)
            remainingTime = 8 - reactionTime;
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. zzz.");
            (this as IPrey).Flee(this, location.x, location.y, "cat");
            remainingTime = 8 - reactionTime;
        }
    }
}
