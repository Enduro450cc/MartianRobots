using System;

namespace MartianRobots
{
    class Houston
    {
        /*
         Assumtions list:
         Commands and responces have no delay
         Movements are made instantly
         No command is lost
         Order of commands is constant
         
         Are command have valid format, invlid command format triggers exception.
         Coordinates and orientation used to send robot are valid and are in range of the Mars grid
         Only latin characters allowed for commands
         50 limit for coordianate
         100 limit for command lenght.
         Unknown command for movement (Except L, R, F) are ignored.

         All commands, coordinates are split by space character, because no way to parse coordinates if 2 digit coordinate is used, ex. 214 E
         Commands should be uppercase

         !If robot is not lost at the end of command sequence, it 'leaves' Mars or just has no interferience with newly sent robots.
         
         It is not specifyed if Robot knows about the edge or no. Depending on this, 3 different scenarios are possible:
             1. Robot is not aware of the edge. When the scent is detected, all following MOVE commands will be ignored.
             2. Robot is aware of the edge and ONLY will fall if the is no scent (I have chosen this one).
             3. Robot is aware of the edge AND current lost position(or the place from where it fell).
         The difference between 2 and 3 occurs in Robots' behavior at the corners:
             At scenario 2, robot will ignore dangerous move because of the scent and edge awarness. Only 1 robot can be lost at the edge.
             At scenario 3, 2 robots can be lost at the corner, using different paths.
             
             */

        static void Main(string[] args)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("5 3");
            sb.AppendLine("1 1 E");
            sb.AppendLine("RFRFRFRF");
            sb.AppendLine("3 2 N");
            sb.AppendLine("FRRFLLFFRRFLL");
            //sb.AppendLine("3 2 N");
            //sb.AppendLine("FRRFLLFFRRFL");
            sb.AppendLine("0 3 W");
            sb.AppendLine("LLFFFLFLFL");

            var input = sb.ToString();
            Mars.InitMarsMission(input);

            var robet = new Robot(null, 0, "weqwe12312312");

            
            Console.ReadKey();

        }
    }
}
