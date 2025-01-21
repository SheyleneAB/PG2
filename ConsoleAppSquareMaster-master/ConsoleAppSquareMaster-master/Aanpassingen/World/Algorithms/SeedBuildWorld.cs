using ConsoleAppSquareMaster.Aanpassingen.World.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.World.Algorithms
{
    internal class SeedBuildWorld : IWorldBuilder
    {
        private readonly Random random = new Random();

        public bool[,] BuildWorld(int maxY, int maxX, params object[] parameters)
        {
            double coverage = (double)parameters[0];
            int seeds = 5;
            bool[,] squares = new bool[maxX, maxY];
            int coverageRequired = (int)(coverage * maxX * maxY);
            int currentCoverage = 0;
            List<(int, int)> list = new();

            for (int i = 0; i < seeds; i++)
            {
                int x = random.Next(maxX);
                int y = random.Next(maxY);
                if (!list.Contains((x, y))) { list.Add((x, y)); currentCoverage++; squares[x, y] = true; }
            }

            while (currentCoverage < coverageRequired)
            {
                int index = random.Next(list.Count);
                int direction = random.Next(4);
                (int x, int y) = list[index];

                if (direction == 0 && x < maxX - 1 && !squares[x + 1, y]) { squares[x + 1, y] = true; list.Add((x + 1, y)); currentCoverage++; }
                else if (direction == 1 && x > 0 && !squares[x - 1, y]) { squares[x - 1, y] = true; list.Add((x - 1, y)); currentCoverage++; }
                else if (direction == 2 && y < maxY - 1 && !squares[x, y + 1]) { squares[x, y + 1] = true; list.Add((x, y + 1)); currentCoverage++; }
                else if (direction == 3 && y > 0 && !squares[x, y - 1]) { squares[x, y - 1] = true; list.Add((x, y - 1)); currentCoverage++; }
            }

            return squares;
        }
    }
}
