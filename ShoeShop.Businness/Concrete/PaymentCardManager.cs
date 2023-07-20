using ShoeShop.Business.Abstract;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ShoeShop.Business.Concrete
{
    public class PaymentCardManager : IPaymentCardService
    {
        private IPaymentCardRepository _paymentCardRepository;

        public PaymentCardManager(IPaymentCardRepository paymentCardRepository)
        {
            _paymentCardRepository = paymentCardRepository;
        }

        public int Add(PaymentCard entity)
        {
            // Implement business rules here
            return _paymentCardRepository.Add(entity);
        }

        public void DeleteById(int id)
        {
            // Implement business rules here
            _paymentCardRepository.DeleteById(id);
        }

        public IList<PaymentCard> GetAll()
        {
            // Implement business rules here
            return _paymentCardRepository.GetAll();
        }

        public PaymentCard GetById(int id)
        {
            // Implement business rules here
            return _paymentCardRepository.GetById(id);
        }

        public List<PaymentCard> GetPaymentCardByUser(int userId)
        {
            return _paymentCardRepository.GetCardByUserId(userId);
        }

        public int Update(PaymentCard entity)
        {
            // Implement business rules here
            return _paymentCardRepository.Update(entity);
        }
    }
}
