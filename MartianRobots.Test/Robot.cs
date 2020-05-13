using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace MartianRobots.Test
{
    /// <summary>
    /// Summary description for Robot
    /// </summary>
    [TestClass]
    public class Robot
    {
        public Robot()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private string marsInitInput = "  5  3";

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SetCoordinates()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            int xCoord = 2;
            int yCoord = 2;
            char orientation = 'E';

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");
            Assert.AreEqual(R2D2.XPos, xCoord);
            Assert.AreEqual(R2D2.YPos, yCoord);

        }

        [TestMethod]
        public void SetOrientation()
        {
            char orientation = 'E';
            TestSetOrientation(orientation);

            orientation = 'W';
            TestSetOrientation(orientation);

            orientation = 'N';
            TestSetOrientation(orientation);

            orientation = 'S';
            TestSetOrientation(orientation);

            orientation = 'e';
            TestSetOrientation(orientation);

            orientation = 'w';
            TestSetOrientation(orientation);

            orientation = 'n';
            TestSetOrientation(orientation);

            orientation = 's';
            TestSetOrientation(orientation);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetWrongOrientation()
        {
            char orientation = 'A';
            TestSetOrientation(orientation);
        }


        private void TestSetOrientation(char orientation)
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            int xCoord = 2;
            int yCoord = 2;

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");
            orientation = Char.ToUpper(orientation, CultureInfo.InvariantCulture);
            Assert.AreEqual(R2D2.Orintation, orientation);
        }

        [TestMethod]
        public void TurnLeft()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            var R2D2 = mars.SendNewRobot(0, "2 2 E");
            Assert.AreEqual(R2D2.Orintation, 'E');

            R2D2.TurnLeft();
            Assert.AreEqual(R2D2.Orintation, 'N');

            R2D2.TurnLeft();
            Assert.AreEqual(R2D2.Orintation, 'W');

            R2D2.TurnLeft();
            Assert.AreEqual(R2D2.Orintation, 'S');

            R2D2.TurnLeft();
            Assert.AreEqual(R2D2.Orintation, 'E');
        }

        [TestMethod]
        public void TurnRight()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            var R2D2 = mars.SendNewRobot(0, "1 2 W");
            Assert.AreEqual(R2D2.Orintation, 'W');

            R2D2.TurnRight();
            Assert.AreEqual(R2D2.Orintation, 'N');

            R2D2.TurnRight();
            Assert.AreEqual(R2D2.Orintation, 'E');

            R2D2.TurnRight();
            Assert.AreEqual(R2D2.Orintation, 'S');

            R2D2.TurnRight();
            Assert.AreEqual(R2D2.Orintation, 'W');
        }

        [TestMethod]
        public void MoveForward()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            int xCoord = 1;
            int yCoord = 2;
            char orientation = 'N';

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");

            R2D2.MoveForward();

            Assert.AreEqual(R2D2.XPos, xCoord);
            Assert.AreEqual(R2D2.YPos, yCoord + 1);
            Assert.AreEqual(R2D2.Orintation, orientation);

        }


        [TestMethod]
        public void SendCommandToRobot()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            int xCoord = 1;
            int yCoord = 1;
            char orientation = 'E';

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");

            R2D2.ProcessInput("RFRFRFRF");

            Assert.AreEqual(R2D2.XPos, xCoord);
            Assert.AreEqual(R2D2.YPos, yCoord);
            Assert.AreEqual(R2D2.Orintation, orientation);
        }

        [TestMethod]
        public void SendCommandToRobot2()
        {
            var input = "30 30";
            var mars = new MartianRobots.Mars(input);

            int xCoord = 3;
            int yCoord = 2;
            char orientation = 'N';

            string robotCommand = "FRRFLLFFRRFLL";

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");
            R2D2.ProcessInput(robotCommand);

            Assert.IsFalse(R2D2.IsLost);
        }


        [TestMethod]
        public void IgnoreUnknownCommand()
        {
            var input = marsInitInput;
            var mars = new MartianRobots.Mars(input);

            int xCoord = 1;
            int yCoord = 1;
            char orientation = 'E';

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");

            R2D2.ProcessInput("WHY_ME?");

            Assert.AreEqual(R2D2.XPos, xCoord);
            Assert.AreEqual(R2D2.YPos, yCoord);
            Assert.AreEqual(R2D2.Orintation, orientation);
            Assert.IsFalse(R2D2.IsLost);
        }

        [TestMethod]
        public void ThisIsSpartha()
        {
            var input = " 5 3";
            var mars = new MartianRobots.Mars(input);

            int xCoord = 3;
            int yCoord = 2;
            char orientation = 'N';
            string robotCommand = "FRRFLLFFRRFLL";


            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");

            R2D2.ProcessInput(robotCommand);
            Assert.IsTrue(R2D2.IsLost);

        }

        [TestMethod]
        public void IgnoreCommandIfLostRobotDetected()
        {
            var input = " 5 3";
            var mars = new MartianRobots.Mars(input);

            int xCoord = 3;
            int yCoord = 2;
            char orientation = 'N';
            string robotCommand = "FRRFLLFFRRFLL";

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");
            R2D2.ProcessInput(robotCommand);

            Assert.IsTrue(R2D2.IsLost);

            var C_3PO = mars.SendNewRobot(1, $"{xCoord} {yCoord} {orientation}");
            C_3PO.ProcessInput(robotCommand);

            Assert.IsFalse(C_3PO.IsLost);
        }


        [TestMethod]
        public void TwoRobotsLostFromCorner()
        {
            var input = " 10   10";
            var mars = new MartianRobots.Mars(input);

            int xCoord = 0;
            int yCoord = 0;
            char orientation = 'S';
            string robotCommand = "FF";

            var R2D2 = mars.SendNewRobot(0, $"{xCoord} {yCoord} {orientation}");
            R2D2.ProcessInput(robotCommand);

            Assert.IsTrue(R2D2.IsLost);

            var C_3PO = mars.SendNewRobot(1, $"{xCoord} {yCoord} {orientation}");
            C_3PO.TurnRight();
            C_3PO.ProcessInput(robotCommand);

            Assert.IsFalse(C_3PO.IsLost);
        }



    }
}
