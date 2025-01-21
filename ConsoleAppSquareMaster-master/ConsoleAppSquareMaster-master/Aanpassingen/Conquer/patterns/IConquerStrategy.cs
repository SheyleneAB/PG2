using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns
{
    internal interface IConquerStrategy
    {
        int[,] Conquer(bool[,] world, int[,] worldempires, int nEmpires, int turns);
    }
}
