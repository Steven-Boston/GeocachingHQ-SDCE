using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Geocache_API.Models;

namespace Geocache_API.Data
{
    public class GeoCacheDbContext : DbContext
    {
        public DbSet<Cache> Caches { get; set; }
        public DbSet<Item> Items { get; set; }
        public GeoCacheDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
