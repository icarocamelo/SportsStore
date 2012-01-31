using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain
{
    public class Product
    {
        //[ScaffoldColumn(false)]
        public int ProductID { get; set; }

        //[Required(ErrorMessage = "You MUST specify the product´s name")]
        public string Name { get; set; }

        //[StringLength(100)]
        public string Description { get; set; }

        //[Required(ErrorMessage = "You MUST specify the price")]
        //[Range(0.01, 1000.00, ErrorMessage="The price MUST be between 0.01 and 1000.00")]
        public decimal Price { get; set; }

        //[StringLength(2)]
        public string Category { get; set; }
    }
}
