
using ConsoleAppSquareMaster.Aanpassingen.World.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.World.patterns
{
    internal class WorldFactory
    {
        public static IWorldBuilder GetWorldBuilder(string type)
        {
            return type switch
            {
                "column" => new ColumnBuildWorld(),
                "seed" => new SeedBuildWorld(),
                _ => throw new ArgumentException("Invalid builder type."),
            };
        }
    }
}
