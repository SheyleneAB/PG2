using ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer
{
    public class WorldConquer
    {
        private bool[,] world;
        private int[,] worldempires;
        private int maxx, maxy;

        public WorldConquer(bool[,] world)
        {
            this.world = world;
            maxx = world.GetLength(0);
            maxy = world.GetLength(1);
            worldempires = new int[maxx, maxy];
            for (int i = 0; i < world.GetLength(0); i++)
                for (int j = 0; j < world.GetLength(1); j++)
                    worldempires[i, j] = world[i, j] ? 0 : -1; 
        }
        public int[,] Conquer(string strategyType, int nEmpires, int turns)
        {
            var strategy = ConquerFactory.CreateStrategy(strategyType);
            strategy.Initialize(world, worldempires); 
            return strategy.Conquer(world, worldempires, nEmpires, turns);
        }
    }
}
