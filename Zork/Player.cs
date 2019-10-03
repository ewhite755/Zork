using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    class Player
    {
        public World World { get; set; }

        public Room Location { get; set; }
        
        public string LocationName
        {
            get
            {
                return Location?.Name;
            }
            set
            {
                Location = World?.Rooms.GetValueOrDefault(value);
            }
        }

        public Player(World world, string startingLocation)
        {
            World = world;
            LocationName = startingLocation;
        }
    }
}
