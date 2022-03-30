using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Models
{
    public class BowlingDbContext :DbContext
    {
        public BowlingDbContext(DbContextOptions<BowlingDbContext> options): base(options)
        {

        }

        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Team> Teams { get; set; }

  
    }
}
