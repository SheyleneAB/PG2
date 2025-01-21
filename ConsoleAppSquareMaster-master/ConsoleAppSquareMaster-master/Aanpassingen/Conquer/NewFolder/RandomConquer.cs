using ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer.NewFolder
{
    internal class RandomConquer : IConquerStrategy
    {
        private Random random = new Random();

        public int[,] Conquer(bool[,] world, int[,] worldempires, int nEmpires, int turns)
        {
            int maxx = world.GetLength(0);
            int maxy = world.GetLength(1);
            Dictionary<int, List<(int, int)>> empires = InitializeEmpires(world, worldempires, nEmpires);

            for (int i = 0; i < turns; i++)
            {
                foreach (var empire in empires)
                {
                    int index = random.Next(empire.Value.Count);
                    int direction = random.Next(4);
                    var (x, y) = empire.Value[index];
                    TryExpand(world, worldempires, empire.Key, empires[empire.Key], x, y, direction, maxx, maxy);
                }
            }
            return worldempires;
        }

        private void TryExpand(bool[,] world, int[,] worldempires, int empireId, List<(int, int)> empireCells, int x, int y, int direction, int maxx, int maxy)
        {
            int newX = x, newY = y;
            switch (direction)
            {
                case 0: newX++; break; 
                case 1: newX--; break; // Left
                case 2: newY++; break; // Down
                case 3: newY--; break; // Up
            }

            if (IsValidPosition(newX, newY, maxx, maxy) && world[newX, newY] && worldempires[newX, newY] == 0)
            {
                worldempires[newX, newY] = empireId;
                empireCells.Add((newX, newY));
            }
        }

        private Dictionary<int, List<(int, int)>> InitializeEmpires(bool[,] world, int[,] worldempires, int nEmpires)
        {
            Dictionary<int, List<(int, int)>> empires = new();
            int maxx = world.GetLength(0);
            int maxy = world.GetLength(1);
            Random random = new Random();

            for (int i = 0; i < nEmpires; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = random.Next(maxx);
                    int y = random.Next(maxy);
                    if (world[x, y] && worldempires[x, y] == 0)
                    {
                        worldempires[x, y] = i + 1;
                        empires[i + 1] = new List<(int, int)> { (x, y) };
                        placed = true;
                    }
                }
            }
            return empires;
        }

        private bool IsValidPosition(int x, int y, int maxx, int maxy) => x >= 0 && x < maxx && y >= 0 && y < maxy;
    }
}
