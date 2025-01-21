using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.World.patterns
{
    internal interface IWorldBuilder
    {
        bool[,] BuildWorld(int maxY, int maxX, params object[] parameters);
    }
}
