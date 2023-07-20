using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using static ShoeShop.Entities.Concrete.Order;

namespace ShoeShop.Business.Abstract
{
    public interface IOrderService
    {
        int Add(Order entity);
        void DeleteById(int id);
        IList<Order> GetAll();
        Order GetById(int id);
        List<Order> GetOrders(int userId);
        int Update(Order entity);
        Order GetOrderWithProductDetails(string ordernumber);

        Dictionary<bool, string> CheckpaymentByUser(int userId,decimal amount);

        bool ChangeOrderStatus(int orderId, EnumOrderState OrderStatus);
    }
}
