using ShoeShop.Entities.Concrete;
using System.Collections.Generic;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IPaymentCardRepository : IRepository<PaymentCard>
    {
        // Add any additional methods required for PaymentCard operations
        List<PaymentCard> GetCardByUserId(int UserId);
    }
}