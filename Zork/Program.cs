using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            SpawnPlayer("West of House");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[Location.Row, Location.Column]);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork' lies by the door.";
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

        private static void SpawnPlayer(string roomName)
        {
            Location = IndexOf(Rooms, roomName);
            if ((Location.Row, Location.Column) == (-1,-1))
            {
                throw new Exception($"Did not find room: {roomName}");
            }
        }

        private static (int Row, int Column) IndexOf(string[,] values, string valueToFind)
        {
            for (int row = 0; row < values.GetLength(0); row++)
            {
                for (int column = 0; column < values.GetLength(1); column++)
                {
                    if (valueToFind == values[row, column])
                    {
                        return (row, column);
                    }
                }
            }
            return (-1, -1);
        }

        private static readonly List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
        };

        private static readonly string[,] Rooms = {
            { "Rocky Trail", "South of House", "Canyon View" },
            { " Forest", "West of House", "Behind House"},
            { "Dense Woods", "North of House", "Clearing" }
        };

        private static (int Row, int Column) Location;
    }
}

