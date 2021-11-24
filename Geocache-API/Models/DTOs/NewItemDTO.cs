using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.DTOs
{
    public class NewItemDTO
    { 
        [Required]
        public string Name { get; set; }

        public int Cache { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ExpireYear { get; set; }

        [Required]
        public int ExpireMonth { get; set; }

        [Required]
        public int ExpireDay { get; set; }
    }
}
