using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class PaymentMapper
    {
        public Payment MapToPayment(PaymentDTO pd)
        {
            return new Payment
            {
                IdOrder = pd.IdOrder,
                IdUser = pd.IdUser,
                TotalAmount = pd.TotalAmount,
                CreditCard = pd.CreditCart,
                Cash = pd.Cash,
                Bizum = pd.Bizum
            };
        }
    }
}
