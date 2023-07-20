using ShoeShop.Entities.Concrete;
using System.Collections.Generic;

namespace ShoeShop.Business.Abstract
{
    public interface IPaymentCardService
    {
        int Add(PaymentCard entity);
        void DeleteById(int id);
        IList<PaymentCard> GetAll();
        PaymentCard GetById(int id);
        int Update(PaymentCard entity);

        List<PaymentCard> GetPaymentCardByUser(int userId);
    }
}
