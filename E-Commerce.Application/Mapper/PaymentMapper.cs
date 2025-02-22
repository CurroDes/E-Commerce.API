using E_Commerce.Application.DTOs;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class PaymentMapper
    {
        private readonly IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
        public PaymentMapper(IShoppingCartRepository<ShoppingCart> shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Payment> MapToPayment(PaymentDTO pd, IEnumerable<ShoppingCart> shoppingCart)
        {
            decimal totalAmount = 0;
            var paidStatus = "";

            //Comprobamos si el carrito está pagado:
            var unpaidCarts = await _shoppingCartRepository.UnPaidCartAsync(pd.IdOrder);

            foreach (var cart in shoppingCart)
            {
                if (!unpaidCarts.Any())
                {
                    if (cart.UnitAmount.HasValue && cart.Quantity.HasValue)
                    {
                        totalAmount += cart.UnitAmount.Value * cart.Quantity.Value;
                    }
                }
            }

            if (totalAmount == 0)
            {
                paidStatus = "Este carrito ya ha sido pagado. Cobro cancelado";
            }

            if (totalAmount != 0)
            {
                paidStatus = "Paid";
            }

            return new Payment
            {
                IdOrder = pd.IdOrder,
                IdUser = pd.IdUser,
                TotalAmount = totalAmount,
                CreditCard = pd.CreditCart,
                Cash = pd.Cash,
                Bizum = pd.Bizum,
                Status = paidStatus


            };
        }
    }
}
