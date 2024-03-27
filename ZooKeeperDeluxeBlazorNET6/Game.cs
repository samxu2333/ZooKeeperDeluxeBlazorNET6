using System;
using System.Collections.Generic;

namespace ZooKeeperDeluxe
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        static private int maxCellsX = 10;
        static private int maxCellsY = 10;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);

        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }

        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                for (var x = 0; x < numCellsX; x++)
                {
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                numCellsY++;
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList); // It isn't as simple as adding this one line, unfortunately, but it's a start...
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null)); // It isn't as simple as adding this one line, unfortunately, but it's a start...
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
        }

        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            // ? : syntax is a shortcut for providing a value conditionally
            // Before the ? is a condition.
            // Before the : is the result if the condition is true.
            // After the : is the result if the condition is false.
            // Without this shortcut, we'd have to write if/else logic and store the result
            // in a temporary string, which we would then pass to WriteLine.
            // You never need to use this logic if you don't want. It is handy, but can be tough to adjust to!
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddToHolding(string occupantType)
        {
            if (holdingPen.occupant != null) return;
            if (occupantType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (occupantType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (occupantType == "elephant") holdingPen.occupant = new Elephant("Stampy");
            if (occupantType == "grass") holdingPen.occupant = new Grass();
            if (occupantType == "boulder") holdingPen.occupant = new Boulder();
            if (occupantType == "chick") holdingPen.occupant = new Chick("zzz");
            if (occupantType == "raptor") holdingPen.occupant = new Raptor("Wow");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        int existingTime = 1;
                        if (zone.occupant as Mouse != null && ((Mouse)zone.occupant).reactionTime == r && !((Mouse)zone.occupant).acted)
                        {
                            if (r < 6) // activate mouse that aren't set to disappear
                            {
                                ((Mouse)zone.occupant).acted = true;
                                ((Mouse)zone.occupant).Activate();
                                existingTime++;
                                if (existingTime > 2)
                                {
                                    Birth((Mouse)zone.occupant);
                                }
                              
                            }
                            else // bye bye mouse!
                            {
                                Console.WriteLine($"{zone.occupant.emoji} has died");
                                zone.occupant = null;
                            }
                        }
                        if (zone.occupant as Cat != null && ((Cat)zone.occupant).reactionTime == r && !((Cat)zone.occupant).acted)
                        {
                            if (r < 9) // activate cat that aren't set to disappear
                            {
                                ((Cat)zone.occupant).acted = true;
                                ((Cat)zone.occupant).Activate();
                            }
                            else // bye bye cats!
                            {
                                Console.WriteLine($"{zone.occupant.emoji} has died");
                                zone.occupant = null;
                            }
                        }
                        if (zone.occupant as Elephant != null && ((Elephant)zone.occupant).reactionTime == r && !((Elephant)zone.occupant).acted)
                        {
                            if (r < 10) // activate elephant that aren't set to disappear
                            {
                                ((Elephant)zone.occupant).acted = true;
                                ((Elephant)zone.occupant).Activate();
                            }
                            else // bye bye elephants!
                            {
                                Console.WriteLine($"{zone.occupant.emoji} has died");
                                zone.occupant = null;
                            }
                        }
                        if (zone.occupant as Chick != null && ((Chick)zone.occupant).reactionTime == r && !((Chick)zone.occupant).acted)
                        {
                            if (r < 8) // activate chick that aren't set to disappear
                            {
                                ((Chick)zone.occupant).acted = true;
                                ((Chick)zone.occupant).Activate();
                            }
                            else // bye bye chick!
                            {
                                Console.WriteLine($"{zone.occupant.emoji} has died");
                                zone.occupant = null;
                            }
                        }
                        if (zone.occupant as Raptor != null && ((Raptor)zone.occupant).reactionTime == r && !((Raptor)zone.occupant).acted)
                        {
                            if (r < 10) // activate elephant that aren't set to disappear
                            {
                                ((Raptor)zone.occupant).acted = true;
                                ((Raptor)zone.occupant).Activate();
                            }
                            else // bye bye raptors!
                            {
                                Console.WriteLine($"{zone.occupant.emoji} has died");
                                zone.occupant = null;
                            }
                        }
                    }
                }
            }
            for (var y=0; y<numCellsY; y++)
            {
                for (var x = 0; x<numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant as Animal != null)
                    {
                        ((Animal)zone.occupant).acted = false;
                    }
                }
            }
        }

        static public void Birth(Animal reproducer)
        {
            int x = reproducer.location.x;
            int y = reproducer.location.y;

            var zoneUp = animalZones[y-1][x];
            var zoneDown = animalZones[y + 1][x];
            var zoneLeft = animalZones[y][x - 1];
            var zoneRight = animalZones[y][x + 1];
            if (y >= 0 && x >= 0 && y <= numCellsY - 1 && x <= numCellsX - 1)
            {
                if (zoneUp.occupant as Animal == null)
                {
                    zoneUp.occupant = reproducer;
                }
                else if (zoneDown.occupant as Animal == null)
                {
                    zoneDown.occupant = reproducer;
                }
                else if (zoneLeft.occupant as Animal == null)
                {
                    zoneLeft.occupant = reproducer;
                }
                else if (zoneRight.occupant as Animal == null)
                {
                    zoneRight.occupant = reproducer;
                }
            }

        }

        static public bool Seek(int x, int y, Direction d, string target)
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
            if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
            if (animalZones[y][x].occupant == null) return false;
            if (animalZones[y][x].occupant.species == target)
            {
                return true;
            }
            return false;
        }

        static public void Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    animalZones[y - 1][x].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + 1][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - 1].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + 1].occupant = attacker;
                    break;
            }
            animalZones[y][x].occupant = null;
        }

        static public bool Retreat(Animal runner, Direction d)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }
    }
}

