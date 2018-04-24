using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Futbol.Importer.Infrastructure
{
    public class FutbolContext : DbContext
    {
        public FutbolContext(DbContextOptions<FutbolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
