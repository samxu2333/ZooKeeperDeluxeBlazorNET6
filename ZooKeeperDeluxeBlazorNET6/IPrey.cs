namespace ZooKeeperDeluxe
{
    public interface IPrey
    {
        public bool Flee(Animal prey, int x, int y, string predator)
        {
            if (Game.Seek(x, y, Direction.up, predator))
            {
                if (Game.Retreat(prey, Direction.down)) return true;
            }
            if (Game.Seek(x, y, Direction.down, predator))
            {
                if (Game.Retreat(prey, Direction.up)) return true;
            }
            if (Game.Seek(x, y, Direction.left, predator))
            {
                if (Game.Retreat(prey, Direction.right)) return true;
            }
            if (Game.Seek(x, y, Direction.right, predator))
            {
                if (Game.Retreat(prey, Direction.left)) return true;
            }
            return false;
        }
    }
}
