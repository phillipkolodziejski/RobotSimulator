using System;
using System.Collections.Generic;
using System.IO;
/*
 * Created by Phillip Kolodziejski on 25/10/18.
 */

namespace RobotSimulator
{
    public struct Robot
    {
        public string Name;
        public bool Initialised;
        public Location Location;
    }

    public struct Location
    {
        public int X;
        public int Y;
        public string F;
    }
        
    public static class TableBounds
    {   
        const int X = 5;
        const int Y = 5;
    }

   public class RobotSimulator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Robot Simulator");

            RobotSimulator rb = new RobotSimulator();

            if (args.Length > 0)
            {
                if (args[0] == "interactive")
                {
                    Console.WriteLine("Interactive mode started. Please enter commands");
                    rb.InteractiveMode();
                }

                if (args.Length > 1 && args[0] == "automated")
                {
                    Console.WriteLine("Automated mode started.");
                    Console.WriteLine("FILE: " + args[1]);
                    rb.AutomatedMode(args[1]);
                }
            } else {
                rb.AutomatedMode("instructions.txt");
            }
            
        }

        public void AutomatedMode(string filename){

            List<string> instructionSet = ReadInstructionSet(filename);
            Robot robot = new Robot();

            foreach (string instruction in instructionSet)
            {
                //Check for place instruction before starting sequence
                if (instruction.Contains("PLACE"))
                {
                    Console.WriteLine("PLACE instruction found");
                    Location location = ParsePlaceToLocation(instruction);
                    robot = InitialiseRobot(location);
                }

                if (robot.Initialised)
                {

                    switch (instruction)
                    {
                        case "MOVE":
                            Console.WriteLine("MOVE instruction");
                            robot = Move(robot);
                            break;
                        case "LEFT":
                            Console.WriteLine("LEFT instruction");
                            robot = Left(robot);
                            break;
                        case "RIGHT":
                            Console.WriteLine("RIGHT instruction");
                            robot = Right(robot);
                            break;
                        case "REPORT":
                            Console.WriteLine("REPORT instruction");
                            Report(robot);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Skipping " + instruction + " Instruction... Robot not placed or initialised");
                }

            }
        }

        public void InteractiveMode(){

            Robot robot = new Robot();
            bool waitCondition = true;

            while (waitCondition) {
                string instruction = Console.ReadLine();

                //Check for place instruction before starting sequence
                if (instruction.Contains("PLACE"))
                {
                    Console.WriteLine("PLACE instruction found");
                    Location location = ParsePlaceToLocation(instruction);
                    robot = InitialiseRobot(location);
                }

                if (robot.Initialised)
                {

                    switch (instruction)
                    {
                        case "MOVE":
                            Console.WriteLine("MOVE instruction");
                            robot = Move(robot);
                            break;
                        case "LEFT":
                            Console.WriteLine("LEFT instruction");
                            robot = Left(robot);
                            break;
                        case "RIGHT":
                            Console.WriteLine("RIGHT instruction");
                            robot = Right(robot);
                            break;
                        case "REPORT":
                            Console.WriteLine("REPORT instruction");
                            Report(robot);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Skipping " + instruction + " Instruction... Robot not placed or initialised");
                }

            }

        }

        public Robot InitialiseRobot(Location location)
        {
            Console.WriteLine("Initialising Robot");

            Robot robot = new Robot
            {
                Name = "Robocop",
                Location = location
            };

            if (ValidateLocation(location))
            {
                robot.Initialised = true;
            }

            Console.WriteLine("Robot Initialised");

            return robot;
        }

        public static List<string> ReadInstructionSet(string filename)
        {
            List<string> instructionList = new List<string>();

            FileStream fileStream = new FileStream("instructions.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            int counter = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                instructionList.Add(line);
                counter++;
            }

            return instructionList;
        }

        public static Location ParsePlaceToLocation(string locationStr)
        {
            locationStr = locationStr.Replace("PLACE", "").Trim();

            string[] splitLocationStr = locationStr.Split(",");

            Location location = new Location(){
                X = Int32.Parse(splitLocationStr[0]),
                Y = Int32.Parse(splitLocationStr[1]),
                F = splitLocationStr[2]
            };

            return location;
        }

        public static bool Place(Robot robot)
        {
            if (!ValidateLocation(robot.Location))
            {
                Console.WriteLine("Please place the robot on the table.");
                return false;
            }

            return true;
        }

        public Robot Move(Robot robot)
        {
            Location newLocation = robot.Location;

            switch (robot.Location.F)
            {
                case "NORTH":
                    newLocation.Y = robot.Location.Y + 1;
                    break;
                case "EAST":
                    newLocation.X = robot.Location.X + 1;
                    break;
                case "SOUTH":
                    newLocation.Y = robot.Location.Y - 1;
                    break;
                case "WEST":
                    newLocation.X = robot.Location.X - 1;
                    break;
            }

            if (ValidateLocation(newLocation))
            {
                robot.Location = newLocation;
            }
            else
            {
                Console.WriteLine("Robo would fall off the table...Skipping move");
            }

            return robot;
        }

        public static bool ValidateLocation(Location location)
        {
            return location.X >= 0 && location.X <= 5 && location.Y >= 0 && location.Y <= 5;
        }

        public Robot Left(Robot robot)
        {
            switch (robot.Location.F)
            {
                case "NORTH":
                    robot.Location.F = "WEST";
                    break;
                case "WEST":
                    robot.Location.F = "SOUTH";
                    break;
                case "SOUTH":
                    robot.Location.F = "EAST";
                    break;
                case "EAST":
                    robot.Location.F = "NORTH";
                    break;
            }

            return robot;
        }

        public Robot Right(Robot robot)
        {
            switch (robot.Location.F)
            {
                case "NORTH":
                    robot.Location.F = "EAST";
                    break;
                case "EAST":
                    robot.Location.F = "SOUTH";
                    break;
                case "SOUTH":
                    robot.Location.F = "WEST";
                    break;
                case "WEST":
                    robot.Location.F = "NORTH";
                    break;
            }

            return robot;
        }

        public static void Report(Robot robot)
        {
            Console.WriteLine("Output:" + robot.Location.X.ToString() + "," + robot.Location.Y.ToString() + "," + robot.Location.F);
        }
    }
}
