using System;
using System.Collections.Generic;

namespace MartianRobots
{
    public class Mars
    {
        private int _xMaxCoordinate;
        private int _yMaxCoordinate;

        public int GridXMaxCoordinate
        {
            get
            {
                return _xMaxCoordinate;
            }
        }

        public int GridYMaxCoordinate
        {
            get
            {
                return _yMaxCoordinate;
            }
        }

        public List<Robot> Robots = new List<Robot>();

        public Mars(string input)
        {
            input = input.ToUpper();
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input is empty");
            }

            var inputCommands = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (inputCommands.Length == 0)
            {
                throw new ArgumentException("input: no lines");
            }

            SetGridSize(inputCommands[0]);
        }


        public static void InitMarsMission(string input)
        {
            input = input.ToUpper();
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input is empty");
            }

            var inputStrings = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (inputStrings.Length == 0)
            {
                throw new ArgumentException("input: no lines");
            }

            var mars = new Mars(inputStrings[0]);

            for (int commandIndex = 1; commandIndex < inputStrings.Length - 1; commandIndex += 2)
            {
                var robot = mars.SendNewRobot(commandIndex - 1, inputStrings[commandIndex]);
                var result = robot.ProcessInput(inputStrings[commandIndex + 1]);
                Console.WriteLine(result);
            }
        }

        public Robot SendNewRobot(int id, string command)
        {
            command = command.ToUpper();
            var robot = new Robot(this, id, command);
            Robots.Add(robot);
            return robot;
        }

        private void SetGridSize(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input is empty");
            }

            //assumption: no whitespaces except char(32)
            var coordinates = input.Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (coordinates.Length != 2)
            {
                throw new ArgumentException("Wrong position format");
            }

            if ((!int.TryParse(coordinates[0], out _xMaxCoordinate)) || (!int.TryParse(coordinates[1], out _yMaxCoordinate)))
            {
                throw new ArgumentException("Wrong position format: coordinate should be a number");
            }

            if (_xMaxCoordinate <= 0 || _yMaxCoordinate <=0)
            {
                throw new ArgumentOutOfRangeException("Grid size cannot be 0 or less");
            }
        }

    }
}
