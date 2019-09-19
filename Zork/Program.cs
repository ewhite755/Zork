using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {


       
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[PlayerPosition]);
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
                        outputString = "Unkown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }
        
        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }
        private static bool Move(Commands command)
        {
            
            if (Directions.Contains(command) == false)
            {
                throw new ArgumentException();
            }

            bool movedSuccessfully;
            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                    movedSuccessfully = false;
                    break;

                case Commands.EAST when PlayerPosition < Rooms.Length - 1:
                    PlayerPosition++;
                    movedSuccessfully = true;
                    break;

                case Commands.WEST when PlayerPosition > 0:
                    PlayerPosition--;
                    movedSuccessfully = true;
                    break;

                default:
                    movedSuccessfully = false;
                    break;
            }
            return movedSuccessfully;
        }


        private static readonly List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
        };
        private static readonly string[] Rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int PlayerPosition = 1;
    }
}
