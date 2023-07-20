using ShoeShop.Entities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using ShoeShop.Entities.Abstract;

namespace ShopShop.Models
{
    public class OrderModel
    {
   
        public int OrderId { get; set; }

     
        public int UserID { get; set; }
        public string OrderNumber { get; set; }

       
        public DateTime OrderDate { get; set; }



        public double TotalPrice { get; set; }

        public string FullName { get; set; }

      
        public string Address { get; set; }
     
        public string Semt { get; set; }
       
        public string Sehir { get; set; }
       
        public string PostaKodu { get; set; }

        public User User { get; set; }


        public List<OrderItemModel> OrderItems { get; set; }
    }

    public class OrderItemModel
    {


        public class OrderItem 
        {
          
            public int ProductId { get; set; }

          
            public string ProductName { get; set; }

         
   
            public int Quantity { get; set; }

           public string Image { get; set; }
        }

    }
}
