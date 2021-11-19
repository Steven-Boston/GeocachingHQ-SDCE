using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.DTOs
{
    public class CacheDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double lat { get; set; }

        public double lon { get; set; }

        public int itemCount { get; set; }

        public List<CacheItem> Items { get; set; }
    }

    public class CacheItem
    {

    }
}
