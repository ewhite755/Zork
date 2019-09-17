using System;

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
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string response;
                switch (command)
                {
                    case Commands.QUIT:
                        response = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        response = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        response = $"You moved {command}.";
                        break;

                    default:
                        response = "Unkown command.";
                        break;
                }

                Console.WriteLine(response);
            }
        }
        
        private static  Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }
    }
}
