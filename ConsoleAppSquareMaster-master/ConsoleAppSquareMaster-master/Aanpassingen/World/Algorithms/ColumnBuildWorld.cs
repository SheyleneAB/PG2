using ConsoleAppSquareMaster.Aanpassingen.World.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.World.Algorithms
{
    internal class ColumnBuildWorld : IWorldBuilder
    {

        private readonly Random random = new Random(10);
        private int maxRandom = 10;
        private int chanceExtra = 6;
        private int chanceLess = 3;

        public bool[,] BuildWorld(int maxY, int maxX, params object[] parameters)
        {
            bool[,] squares = new bool[maxX, maxY];
            int y1 = random.Next(maxY);
            int y2 = random.Next(maxY);
            int yb = Math.Min(y1, y2);
            int ye = Math.Max(y1, y2);

            for (int i = 0; i < maxX; i++)
            {
                for (int j = yb; j < ye; j++) squares[i, j] = true;
                yb = AdjustBoundary(yb, maxY);
                ye = AdjustBoundary(ye, maxY, reverse: true);
                if (ye < yb) break;
            }

            return squares;
        }

        private int AdjustBoundary(int boundary, int max, bool reverse = false)
        {
            int buildResult = Build();

            if (!reverse)
            {
                if (buildResult == 1 && boundary > 0) boundary--;
                else if (buildResult == -1 && boundary < max) boundary++;
            }
            else
            {
                if (buildResult == 1 && boundary < max) boundary++;
                else if (buildResult == -1 && boundary > 0) boundary--;
            }

            return boundary;
        }


        private int Build()
        {
            int x = random.Next(maxRandom);
            if (x > chanceExtra) return 1;
            if (x < chanceLess) return -1;
            return 0;
        }

    }
}
