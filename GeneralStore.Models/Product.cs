using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Models
{
    public class Product
    {
        [Key]
        public string SKU { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int NumberInInventory { get; set; }

        private readonly bool isInStock;
        public bool IsInStock
        {
            get 
            { 
                return isInStock; 
            }
        }

    }
}
