using Microsoft.EntityFrameworkCore;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.DataAccess.Concrete.Data.Repository
{
    public class EfPaymentCardRepository : IPaymentCardRepository
    {
        private readonly ShoeShopDbContext _dbContext;

        public EfPaymentCardRepository(ShoeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(PaymentCard entity)
        {
            _dbContext.PaymentCards.Add(entity);
            return _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var paymentCard = _dbContext.PaymentCards.FirstOrDefault(c => c.ID == id);
            if (paymentCard != null)
            {
                _dbContext.PaymentCards.Remove(paymentCard);
                _dbContext.SaveChanges();
            }
        }

        public IList<PaymentCard> GetAll()
        {
            return _dbContext.PaymentCards
                             .Include(c => c.User)
                             .ToList();
        }

        public PaymentCard GetById(int id)
        {
            return _dbContext.PaymentCards
                             .Include(c => c.User)
                             .FirstOrDefault(c => c.ID == id);
        }


        public List<PaymentCard> GetCardByUserId(int UserId)
        {
            return  _dbContext.PaymentCards
                             .Include(c => c.User)
                             .Where(c => c.UserID == UserId).ToList();
        }
        public int Update(PaymentCard entity)
        {
            _dbContext.PaymentCards.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
