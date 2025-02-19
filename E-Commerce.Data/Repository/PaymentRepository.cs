using E_Commerce.Data.Data;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Repository
{
    public class PaymentRepository<T> : IPaymentRepository<T> where T : Payment
    {
        private readonly ECommerceContext _context;
        public PaymentRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(T payment)
        {
            await _context.AddAsync(payment);
        }
    }
}
