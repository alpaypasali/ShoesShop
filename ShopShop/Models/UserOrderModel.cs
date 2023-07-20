using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Security.Policy;
using static ShoeShop.Entities.Concrete.Order;

namespace ShopShop.Models
{
    public class UserOrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } 
        public int Total { get; set; }
       
        public DateTime OrderDate { get; set; }
        public  EnumOrderState orderState{ get;set; }
        public UserDto User { get; set; }
        public bool Isa { get; set; }
    }
}
