using CakeCompany.Models.Enums;
using System;

namespace CakeCompany.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Cake Cake { get; set; }
        public double Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
