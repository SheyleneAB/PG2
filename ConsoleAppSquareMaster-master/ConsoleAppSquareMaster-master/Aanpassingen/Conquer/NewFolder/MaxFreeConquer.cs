using ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns;
using ConsoleAppSquareMaster.Aanpassingen.World;
using System;
using System.Collections.Generic;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer.NewFolder
{
    internal class MaxFreeConquer : IConquerStrategy
    {
        private bool[,] world;
        private int[,] worldempires;
        private int maxx, maxy;
        private Random random = new Random(1);

        public int[,] Conquer(bool[,] world, int[,] worldempires, int nEmpires, int turns)
        {
            this.world = world;
            this.worldempires = worldempires;
            this.maxx = world.GetLength(0);
            this.maxy = world.GetLength(1);

            Dictionary<int, List<(int, int)>> empires = new(); // key: empire ID, value: cells controlled

            // Initialize start positions
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    int x = random.Next(maxx);
                    int y = random.Next(maxy);
                    if (world[x, y])
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)> { (x, y) });
                    }
                }
            }

            // Simulate turns
            for (int i = 0; i < turns; i++)
            {
                for (int e = 1; e <= nEmpires; e++)
                {
                    int index = FindWithMostEmptyNeighbours(e, empires[e]);
                    int direction = random.Next(4);
                    int x = empires[e][index].Item1;
                    int y = empires[e][index].Item2;

                    switch (direction)
                    {
                        case 0: // Right
                            if (x < maxx - 1 && worldempires[x + 1, y] == 0)
                            {
                                worldempires[x + 1, y] = e;
                                empires[e].Add((x + 1, y));
                            }
                            break;
                        case 1: // Left
                            if (x > 0 && worldempires[x - 1, y] == 0)
                            {
                                worldempires[x - 1, y] = e;
                                empires[e].Add((x - 1, y));
                            }
                            break;
                        case 2: // Down
                            if (y < maxy - 1 && worldempires[x, y + 1] == 0)
                            {
                                worldempires[x, y + 1] = e;
                                empires[e].Add((x, y + 1));
                            }
                            break;
                        case 3: // Up
                            if (y > 0 && worldempires[x, y - 1] == 0)
                            {
                                worldempires[x, y - 1] = e;
                                empires[e].Add((x, y - 1));
                            }
                            break;
                    }
                }
            }

            return worldempires;
        }

        private int FindWithMostEmptyNeighbours(int e, List<(int, int)> empire)
        {
            List<int> indexes = new();
            int maxEmptyNeighbours = 0;

            for (int i = 0; i < empire.Count; i++)
            {
                int emptyNeighbours = EmptyNeighbours(empire[i].Item1, empire[i].Item2);
                if (emptyNeighbours >= maxEmptyNeighbours)
                {
                    if (emptyNeighbours > maxEmptyNeighbours)
                    {
                        indexes.Clear();
                        maxEmptyNeighbours = emptyNeighbours;
                    }
                    indexes.Add(i);
                }
            }

            return indexes[random.Next(indexes.Count)];
        }

        private int EmptyNeighbours(int x, int y)
        {
            int count = 0;
            if (IsValidPosition(x - 1, y) && worldempires[x - 1, y] == 0) count++;
            if (IsValidPosition(x + 1, y) && worldempires[x + 1, y] == 0) count++;
            if (IsValidPosition(x, y - 1) && worldempires[x, y - 1] == 0) count++;
            if (IsValidPosition(x, y + 1) && worldempires[x, y + 1] == 0) count++;
            return count;
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < maxx && y >= 0 && y < maxy;
        }
    }
}

