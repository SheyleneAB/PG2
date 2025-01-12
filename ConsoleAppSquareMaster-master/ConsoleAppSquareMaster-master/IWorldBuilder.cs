using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    internal interface IWorldBuilder
    {
        bool[,] BuildWorld(int maxy, int maxx, double coverage = 0.5);
    }
}
