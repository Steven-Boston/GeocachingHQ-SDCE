using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.DTOs
{
    public class NewItemDTO
    { 
        public string Name { get; set; }

        public int Cache { get; set; }

        public string Description { get; set; }

        public int ExpireYear { get; set; }

        public int ExpireMonth { get; set; }

        public int ExpireDay { get; set; }
    }
}
