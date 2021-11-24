using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Geocache_API.Data;
using Microsoft.EntityFrameworkCore;

namespace GeocacheTests
{
    public class Mock : IDisposable
    {
        public readonly SqliteConnection _connection;
        public readonly GeoCacheDbContext _dbContext;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _dbContext = new GeoCacheDbContext(
                new DbContextOptionsBuilder<GeoCacheDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection?.Dispose();
        }
    }
}
