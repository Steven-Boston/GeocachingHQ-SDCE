using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public int Name { get; set; }

        public int Cache { get; set; }

        public string Description { get; set; }
    }
}
