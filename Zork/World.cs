using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public class World
    {
        public string StartingLocation { get; set; }

        public HashSet<Room> Rooms { get; set; }

        public Player SpawnPlayer()
        {
            return new Player(this, StartingLocation);
        }
    }
}
