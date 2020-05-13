using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobots.Test
{
    [TestClass]
    public class Mars
    {
        [TestMethod]
        public void TestNewMarsObject()
        {
            var input = "5 3";
            new MartianRobots.Mars(input);
        }

        public void TestNewMarsObject2()
        {
            var input = "   5  3 ";
            new MartianRobots.Mars(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewMarsObjectWrongInput()
        {
            var input = "5 3 E";
            new MartianRobots.Mars(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNewMarsObjectOutOfRange()
        {
            var input = "5 -1 ";
            new MartianRobots.Mars(input);
        }

        [TestMethod]
        public void TestMarsInit()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("5 3");
            sb.AppendLine("1 1 E");
            sb.AppendLine("RFRFRFRF");
            sb.AppendLine("3 2 N");
            sb.AppendLine("FRRFLLFFRRFLL");
            sb.AppendLine("0 3 W");
            sb.AppendLine("LLFFFLFLFL");

            var input = sb.ToString();

            MartianRobots.Mars.InitMarsMission(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMarsInitWrongFormat()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("5 3");
            sb.AppendLine("1 1 E");
            sb.AppendLine("RFRFRFRF");
            sb.AppendLine("3 2 N");
            sb.AppendLine("FRRFLLFFRRFLL");
            sb.AppendLine("03 W");
            sb.AppendLine("LLFFFLFLFL");

            var input = sb.ToString();

            MartianRobots.Mars.InitMarsMission(input);

        }


    }
}
