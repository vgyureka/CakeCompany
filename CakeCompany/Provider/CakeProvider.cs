using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider.Interfaces;
using System;

namespace CakeCompany.Provider
{
    public class CakeProvider : ICakeProvider
    {
        public DateTime Check(Order order)
        {
            if (order.Name == Cake.Chocolate)
            {
                return DateTime.Now.Add(TimeSpan.FromMinutes(30));
            }

            if (order.Name == Cake.RedVelvet)
            {
                return DateTime.Now.Add(TimeSpan.FromMinutes(60));
            }

            return DateTime.Now.Add(TimeSpan.FromHours(15));
        }

        public Product Bake(Order order)
        {
            return new()
            {
                Cake = order.Name,
                Id = new Guid(),
                Quantity = order.Quantity
            };
        }
    }
}
