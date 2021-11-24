using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Geocache_API.Models.DTOs
{
    public class NewCacheDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double lat { get; set; }

        [Required]
        public double lon { get; set; }
    }
}
