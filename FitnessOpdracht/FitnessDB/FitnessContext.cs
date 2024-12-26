using FitnessDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FitnessDomein.Model;

namespace FitnessDL
{
    
    public class FitnessContext : DbContext
    {
        
        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }

        
        private string connectionString;
        public DbSet<MemberEF> Member { get; set; }
        public DbSet<ProgramEF> Program { get; set; }
        public DbSet<EquipmentEF> Equipment { get; internal set; }

        public FitnessContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        
    }
        
}