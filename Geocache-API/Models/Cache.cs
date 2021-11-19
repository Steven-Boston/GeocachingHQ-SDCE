using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models
{
    public class Cache
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double lat { get; set; }
        
        [Required]
        public double lon { get; set; }
        
        public int itemCount { get; set; }
    }
}
