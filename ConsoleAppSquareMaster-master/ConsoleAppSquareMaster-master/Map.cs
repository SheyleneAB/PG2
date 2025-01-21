using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class Map
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; } 
        public string Algoritme { get; set; }
        public int Width { get; set; } 
        public int Height { get; set; } 
        public double BedekkingsGraad { get; set; } 
        public int[,] WorldArray { get; set; } 
    }
}
