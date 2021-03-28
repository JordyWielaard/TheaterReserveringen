using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterReservering.Models;

namespace TheaterReservering.Data
{
    public class TheaterContext : DbContext
    {
        public TheaterContext(DbContextOptions<TheaterContext> options) : base(options) { }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Reservering> Reserveringen { get; set; }
    }
}