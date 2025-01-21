using ConsoleAppSquareMaster.Aanpassingen.Conquer;
using ConsoleAppSquareMaster.Aanpassingen.World;
using System;

namespace ConsoleAppSquareMaster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World("seed");
            //var w=world.BuildWorld2(100,100,0.60);
            var w = world.BuildWorld(100, 100);
            DisplayWorld(w);
            
            WorldConquer wq = new WorldConquer(w);
            var ww = wq.Conquer("Random", 5, 25000);
            DisplayConqueredWorld(ww);

            BitmapWriter bmw = new BitmapWriter();
            bmw.DrawWorld(ww);

            var map = new Map
            {
                Width = w.GetLength(0),
                Height = w.GetLength(1),
                
            };
            string connectionString = "mongodb://localhost:27017";
            var databaseService = new DatabaseServer(connectionString);
            databaseService.InsertMap(map);
            Console.WriteLine("Map saved to MongoDB!");
        }
        static void DisplayWorld(bool[,] world)
        {
            for (int i = 0; i < world.GetLength(1); i++)
            {
                for (int j = 0; j < world.GetLength(0); j++)
                {
                    char ch = world[j, i] ? '*' : ' ';
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }
        static void DisplayConqueredWorld(int[,] world)
        {
            for (int i = 0; i < world.GetLength(1); i++)
            {
                for (int j = 0; j < world.GetLength(0); j++)
                {
                    string ch = world[j, i] switch
                    {
                        -1 => " ",
                        0 => ".",
                        _ => world[j, i].ToString()
                    };
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }

    }
}
