using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MartianRobots
{
    public class Robot
    {
        private int _id = 0;
        private int _xPos;
        private int _yPos;
        private int _xPosLastSafe;
        private int _yPosLastSafe;
        private int _orientationIndex;
        private Mars _mars;
        readonly string _orient = "NESW";

        bool _isLost = false;

        public bool IsLost
        {
            get
            {
                return _isLost;
            }
        }

        public int XPos
        {
            get
            {
                return _xPos;
            }
        }

        public int YPos
        {
            get
            {
                return _yPos;
            }
        }
        public char Orintation
        {
            get
            {
                // dangerous, but assume no problems will be here for now, no check
                return _orient[_orientationIndex];
            }
        }

        private Robot() { }

        public Robot(Mars mars, int id, string input)
        {
            InitRobot(mars, id, input);
        }

        private void InitRobot(Mars mars, int id, string input)
        {
            if (mars == null)
            {
                throw new ArgumentNullException("mars paramenter cannot be null");
            }

            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input is empty");
            }

            input = input.ToUpper();
            _id = id;
            _mars = mars;

            //assumption: no whitespaces except char(32)
            var coordinates = input.Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (coordinates.Length != 3)
            {
                throw new ArgumentException("Invalid number of parameters");
            }

            if (int.TryParse(coordinates[0], out _xPos) && int.TryParse(coordinates[1], out _yPos))
            {
                // check coords for being withing range
                if (string.IsNullOrWhiteSpace(coordinates[2]))
                {
                    throw new ArgumentException("Orientation missing");
                }

                _orientationIndex = _orient.IndexOf(coordinates[2][0]);

                if (_orientationIndex < 0)
                {
                    throw new ArgumentException("Invalid Orientation");
                }
            }
            else
            {
                throw new ArgumentException("Wrong position format: coordinate should be a number");
            }
        }

        public string ProcessInput(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input is empty");
            }

            foreach (var command in input)
            {
                if (IsLost) break;
                ProcessCommand(command);
            }

            string result = IsLost ? $"{_xPosLastSafe} {_yPosLastSafe} {Orintation}" : $"{_xPos} {_yPos} {Orintation}";
            if (IsLost)
            {
                result += " LOST";
            }
            return result;
        }

        public void ProcessCommand(char command)
        {
            switch (command)
            {
                case 'R':
                    TurnRight();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                case 'F':
                    MoveForward();
                    break;

                // add code here for processing ediitional commands in the future
                default:
                    // unknown command
                    break;
            }
        }

        public void TurnLeft()
        {
            Turn(-1);
        }

        public void TurnRight()
        {
            Turn(1);
        }

        // Argument is limited to -1 for left and +1 for right. can be improved to support turn around (-2 or +2)
        private void Turn(int turnIndex)
        {
            if (turnIndex != 1 && turnIndex != -1)
            {
                throw new ArgumentOutOfRangeException("turnIndex can be -1 or +1 only");
            }

            _orientationIndex += turnIndex;
            if (_orientationIndex == -1)
            {
                _orientationIndex = _orient.Length - 1;
            }
            if (_orientationIndex == 4)
            {
                _orientationIndex = 0;
            }
        }


        public void MoveForward()
        {
            var orentation = Orintation;
            var xPosNew = _xPos;
            var yPosNew = _yPos;

            switch (orentation)
            {
                case 'N':
                    yPosNew++;
                    break;
                case 'E':
                    xPosNew++;
                    break;
                case 'S':
                    yPosNew--;
                    break;
                case 'W':
                    xPosNew--;
                    break;
            }

            // Check for "Scent"
            var lostRobotScentDetected = _mars.Robots.Any(r => r._id != _id && r.IsLost && r._xPosLastSafe == _xPos && r._yPosLastSafe == _yPos);

            if (WillGetLostAtPosition(xPosNew, yPosNew))
            {
                if (lostRobotScentDetected)
                {
                    // ignore dangerous move
                    return;
                }
                else
                {
                    // no scent, robot gets lost
                    _isLost = true;
                    // leave scent
                    _xPosLastSafe = _xPos;
                    _yPosLastSafe = _yPos;
                }
            }
            else
            {
                // move
                _xPos = xPosNew;
                _yPos = yPosNew;
                // move is safe
                _xPosLastSafe = _xPos;
                _yPosLastSafe = _yPos;
            }
        }

        private bool WillGetLostAtPosition(int xPosition, int yPosition)
        {
            return (xPosition < 0 || yPosition < 0 || xPosition > _mars.GridXMaxCoordinate || yPosition > _mars.GridYMaxCoordinate);
        }

    }
}
