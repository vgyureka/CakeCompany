using CakeCompany.Models;
using CakeCompany.Models.Enums;
using CakeCompany.Provider.Interfaces;
using System;

namespace CakeCompany.Provider
{
    public class OrderProvider : IOrderProvider
    {
        public Order[] GetLatestOrders()
        {
            return new Order[]
            {
                new Order ( "CakeBox", "cakebox@gmail.com",  DateTime.Now,  1,  Cake.RedVelvet,  1, 120.25 ),
                new Order ( "ImportantCakeShop", "importantCakeShop@gmail.com",  DateTime.Now,  2,  Cake.Chocolate,  1, 140.25 )
            };
        }

        public void UpdateOrders(Order[] orders)
        {
        }
    }
}
