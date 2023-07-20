using ShoeShop.Business.Abstract;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.DataAccess.Concrete.Repository;
using ShoeShop.Entities.Abstract;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IPaymentCardService _paymentCardService;
        private IProductRepository _productRepository;
        public OrderManager(IOrderRepository orderRepository, IPaymentCardService paymentCardService, IProductRepository productRepository) // Yeni repository constructor'da inject edildi
        {
            _orderRepository = orderRepository;
            _paymentCardService = paymentCardService;
            _productRepository = productRepository; // Yeni repository nesnesi atandı
        }
        public int Add(Order entity)
        {
            // Implement business rules here
            return _orderRepository.Add(entity);
        }

        public bool ChangeOrderStatus(int orderId, Order.EnumOrderState OrderStatus)
        {
            var Order = _orderRepository.GetById(orderId);
            if(Order is not null)
            {
                Order.OrderState = OrderStatus;
                var a = _orderRepository.Update(Order);
                return a > 0;
            }
            return false;
        }

        public Dictionary<bool, string> CheckpaymentByUser(int userId,decimal amount)
        {
            var dic = new Dictionary<bool, string>();
            var UserCard = _paymentCardService.GetPaymentCardByUser(userId);
            if (UserCard.Any())
            {
                var selectCard = UserCard.FirstOrDefault(x => x.Amount > amount);
                if(selectCard != null)
                {
                    selectCard.Amount = (int)(selectCard.Amount- amount);
                    _paymentCardService.Update(selectCard);
                    dic.Add(true, "Ok");
                    return dic;
                }
                dic.Add(true, "Not Balance");
            }
            else dic.Add(true, "Not Found User Card");
            return dic;
        }

        public void DeleteById(int id)
        {
            // Implement business rules here
            _orderRepository.DeleteById(id);
        }

        public IList<Order> GetAll()
        {
            // Implement business rules here
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            // Implement business rules here
            return _orderRepository.GetById(id);
        }

        public List<Order> GetOrders(int userId)
        {
            // Implement business rules here
            return _orderRepository.GetOrders(userId);
        }

        public int Update(Order entity)
        {
            // Implement business rules here
            return _orderRepository.Update(entity);
        }
        public Order GetOrderWithProductDetails(string orderNumber)
        {
            var order = _orderRepository.GetOrderWithProductDetails(orderNumber);
            return order;
        }
    }
}
