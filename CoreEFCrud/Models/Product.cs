using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreEFCrud.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
