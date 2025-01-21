using ConsoleAppSquareMaster.Aanpassingen.Conquer.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Aanpassingen.Conquer.patterns
{
    internal class ConquerFactory
    {
        public static IConquerStrategy CreateStrategy(string strategyType)
        {
            return strategyType switch
            {
                "Random" => new RandomConquer(),
                "MaxFree" => new MaxFreeConquer(),
                "Free" => new FreeConquer(),
                _ => throw new ArgumentException($"Unknown strategy type: {strategyType}")
            };
        }
    }
}
