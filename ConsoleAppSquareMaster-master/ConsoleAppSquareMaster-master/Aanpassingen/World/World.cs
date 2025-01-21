using ConsoleAppSquareMaster.Aanpassingen.World.patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.World
{
    public class World
    {
        private IWorldBuilder worldBuilder;

        public World(string builderType)
        {
            worldBuilder = WorldFactory.GetWorldBuilder(builderType);
        }

        public bool[,] BuildWorld(int maxY, int maxX, params object[] parameters)
        {
            return worldBuilder.BuildWorld(maxY, maxX, parameters);
        }
    }
}
