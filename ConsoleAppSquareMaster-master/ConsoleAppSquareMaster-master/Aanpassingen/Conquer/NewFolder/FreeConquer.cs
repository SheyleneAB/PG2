using ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer.NewFolder
{
    internal class FreeConquer : IConquerStrategy
    {
        private Random random = new Random(1);

        public int[,] Conquer(bool[,] world, int[,] worldempires, int nEmpires, int turns)
        {
            int maxx = world.GetLength(0);
            int maxy = world.GetLength(1);

            Dictionary<int, List<(int, int)>> empires = new(); //key is the empire id, value is the list of cells (x,y) the empire controls

            // Search random start positions of each empire
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    int x = random.Next(maxx);
                    int y = random.Next(maxy);
                    if (world[x, y] && worldempires[x, y] == 0)
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)>() { (x, y) });
                    }
                }
            }

            for (int i = 0; i < turns; i++)
            {
                for (int e = 1; e <= nEmpires; e++)
                {
                    if (empires[e].Count > 0)
                    {
                        int index = random.Next(empires[e].Count);
                        pickEmpty(empires[e], index, e, world, worldempires, maxx, maxy);
                    }
                }
            }

            return worldempires;
        }

        private void pickEmpty(List<(int, int)> empire, int index, int e, bool[,] world, int[,] worldempires, int maxx, int maxy)
        {
            List<(int, int)> n = new();

            int x = empire[index].Item1;
            int y = empire[index].Item2;

            if (IsValidPosition(x - 1, y, world, worldempires, maxx, maxy)) n.Add((x - 1, y));
            if (IsValidPosition(x + 1, y, world, worldempires, maxx, maxy)) n.Add((x + 1, y));
            if (IsValidPosition(x, y - 1, world, worldempires, maxx, maxy)) n.Add((x, y - 1));
            if (IsValidPosition(x, y + 1, world, worldempires, maxx, maxy)) n.Add((x, y + 1));

            if (n.Count > 0)
            {
                var newLocation = n[random.Next(n.Count)];
                empire.Add(newLocation);
                worldempires[newLocation.Item1, newLocation.Item2] = e;
            }
        }

        private bool IsValidPosition(int x, int y, bool[,] world, int[,] worldempires, int maxx, int maxy)
        {
            return x >= 0 && x < maxx && y >= 0 && y < maxy && world[x, y] && worldempires[x, y] == 0;
        }
    }
}
