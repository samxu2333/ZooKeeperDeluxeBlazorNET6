using System;
namespace ZooKeeperDeluxe
{
    public class Animal : Occupant
    {
        public string name;
        public int reactionTime = 5; // default reaction time for animals (for this game, range is 1 - 9, with 10 being dead)
        public bool acted = false; // has the animal acted yet this turn?
        public int remainingTime = 0; //Times remaning for animals before death.
        public int existingTime = 0;
        public int ReproductionInterval = 0;
        public int TurnsSinceLastReproduction = 0;
        public bool HasReproduced = false;

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


        // Method to handle reproduction logic
        public virtual void Birth(Zone[,] animalZones, int y, int x, int reproductionInterval)
        {
            ReproductionInterval = reproductionInterval;
            // Check if the animal has reproduced already
            if (HasReproduced)
                return;

            // Check if it's time for reproduction
            if (TurnsSinceLastReproduction >= ReproductionInterval)
            {
                // Check adjacent zones for empty space
                if (y > 0 && animalZones[y - 1, x].occupant == null)
                {
                    animalZones[y - 1, x].occupant = new Mouse("mouse"); // Create a copy of the animal
                    HasReproduced = true; // Set flag to indicate reproduction
                    return;
                }
                if (y > 0 && animalZones[y + 1, x].occupant == null)
                {
                    animalZones[y + 1, x].occupant = new Mouse("mouse"); // Create a copy of the animal
                    HasReproduced = true; // Set flag to indicate reproduction
                    return;
                }
                if (x > 0 && animalZones[y, x-1].occupant == null)
                {
                    animalZones[y, x-1].occupant = new Mouse("mouse"); // Create a copy of the animal
                    HasReproduced = true; // Set flag to indicate reproduction
                    return;
                }
                if (x > 0 && animalZones[y, x+1].occupant == null)
                {
                    animalZones[y, x+1].occupant = new Mouse("mouse"); // Create a copy of the animal
                    HasReproduced = true; // Set flag to indicate reproduction
                    return;
                }


                // If no adjacent empty space is found, reset reproduction timer
                TurnsSinceLastReproduction = 0;
            }
            else
            {
                // Increment the turns since last reproduction
                TurnsSinceLastReproduction++;
            }
        }
    }

}
