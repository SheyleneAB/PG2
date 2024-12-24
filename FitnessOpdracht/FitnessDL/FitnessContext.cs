using FitnessDL.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDL
{
    public class FitnessContext : DbContext
    {
        private string connectionString;
        public DbSet<MemberEF> Member { get; set; }
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