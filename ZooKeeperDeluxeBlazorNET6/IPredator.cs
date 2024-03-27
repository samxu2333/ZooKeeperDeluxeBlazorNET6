namespace ZooKeeperDeluxe
{
    public interface IPredator
    {
        public bool Hunt(Animal predator, int x, int y, string prey)
        {
            if (Game.Seek(x, y, Direction.up, prey))
            {
                Game.Attack(predator, Direction.up);
                return true;
            }
            else if (Game.Seek(x, y, Direction.down, prey))
            {
                Game.Attack(predator, Direction.down);
                return true;
            }
            else if (Game.Seek(x, y, Direction.left, prey))
            {
                Game.Attack(predator, Direction.left);
                return true;
            }
            else if (Game.Seek(x, y, Direction.right, prey))
            {
                Game.Attack(predator, Direction.right);
                return true;
            }
            return false;
        }
    }
}
