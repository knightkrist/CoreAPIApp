using CoreAPIApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIApp.Data
{
    public class TripContext :DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer(@"Server;Database;trusted_connection");
        }*/


        public TripContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Segment> Segments { get; set; }
    }
}
