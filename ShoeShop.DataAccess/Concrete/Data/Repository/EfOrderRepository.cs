using Microsoft.EntityFrameworkCore;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Concrete.Data.Repository
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly ShoeShopDbContext _dbContext;

        public EfOrderRepository(ShoeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            return _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.ID == id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
        }

        public IList<Order> GetAll()
        {
            return _dbContext.Orders
                            .Include(i => i.OrderItems)
                            .ThenInclude(i => i.Product)
                            .ToList();
        }

        public Order GetById(int id)
        {
            return _dbContext.Orders
                            .Include(i => i.OrderItems)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault(o => o.ID == id);
        }

        public List<Order> GetOrders(int userId)
        {
            var orders = _dbContext.Orders
                                     .Include(i => i.OrderItems)
                                     .ThenInclude(i => i.Product)
                                     .Where(i => i.UserID == userId)
                                     .ToList();

            return orders;
        }

        public int Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            return _dbContext.SaveChanges();
        }

        public Order GetOrderWithProductDetails(string orderNumber)
        {
            var order = _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderNumber == orderNumber);
            return order;
        }
    }
}
