using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Dtos.Concrete
{
    public class OrderDTO
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string PostaKodu { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
