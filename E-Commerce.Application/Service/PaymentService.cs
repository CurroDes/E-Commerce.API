using Castle.Core.Logging;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Mapper;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class PaymentService
    {
        private readonly PaymentMapper _paymentMapper;
        private readonly ILogger<PaymentService> _logger;
        private readonly IOrderRepository<Order> _orderRepository;

        public PaymentService(PaymentMapper paymentMapper, ILogger<PaymentService> logger, IOrderRepository<Order> orderRepository)
        {
            _paymentMapper = paymentMapper;
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<Result> AddPaymentService(PaymentDTO p)
        {
            Result result = new Result();

            try
            {
                //Verificar el order en el cual se va a proceder a realizar el pago.
                var payment = _paymentMapper.MapToPayment(p);

                if (payment == null)
                {
                    result.Success = false;
                    result.Error = $"Error al intentar pagar para la orden: {p.IdOrder} para el usuario {p.IdUser}";
                    _logger.LogError(result.Error.ToString());

                    return result;
                }
                //Comprobar el usuario ligado a ese pago -desde el order?
                var user = await _orderRepository.IsOrderPlacedByUserAsync(p.IdOrder, p.IdUser);

                if (!user)
                {
                    result.Success = false;
                    result.Error = $"El usuario no se corresponde al pedido seleccionado";
                    _logger.LogError(result.Error.ToString());
                }

                var paymentIsTrue = await _orderRepository.IsOrdenPayment(p.IdOrder);

                if (paymentIsTrue)
                {
                    
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
