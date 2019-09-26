﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
            set
            {
            }
        }
        static void Main(string[] args)
        {
            InitializeRoomDescription();
            Console.WriteLine("Welcome to Zork!");

            Location = IndexOf(Rooms, "West of House");
            Assert.IsTrue(Location != (-1, -1));

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = (CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool movedSuccessfully = Move(command);
                        if (movedSuccessfully)
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut";
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN);


        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid Direction.");

            bool movedSuccessfully = true;
            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.SOUTH when Location.Column > 0:
                    Location.Row--;
                    break;


                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    movedSuccessfully = false;
                    break;
            }
            return movedSuccessfully;
        }

        private static (int Row, int Column) IndexOf(Room[,] values, string valueToFind)
        {
            for (int row = 0; row < values.GetLength(0); row++)
            {
                for (int column = 0; column < values.GetLength(1); column++)
                {
                    if (valueToFind == values[row, column].Name)
                    {
                        return (row, column);
                    }
                }
            }
            return (-1, -1);
        }

        private static void InitializeRoomDescription()
        {
            Rooms[0, 0].Description = "You are on a rock-strewn trail.";
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
            Rooms[0, 2].Description = "You are at the top of Great Canyon on its south wall.";

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";
            Rooms[1, 1].Description = "This is an open field west of a white house with a boarded front door";
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";
                                     
            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
            Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
        }

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
        };

        private static readonly Room[,] Rooms = 
        {
            { new Room("Rocky Trail"),    new Room("South of House"),   new Room("Canyon View")     },
            { new Room("Forest"),         new Room("West of House"),    new Room("Behind House")    },
            { new Room("Dense Woods"),    new Room("North of House"),   new Room("Clearing")        }
        };

        private static (int Row, int Column) Location;
    }
}

