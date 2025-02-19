using Castle.Core.Logging;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Mapper;
using E_Commerce.Application.UnitOfWork;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using E_Commerce.Data.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentMapper _paymentMapper;
        private readonly ILogger<PaymentService> _logger;
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IShoppingCartRepository<ShoppingCart> _shoppingCartRepository;
        private readonly ShoppingCartMapper _shoppingCartMapper;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IPaymentRepository<Payment> _paymentRepository;

        public PaymentService(PaymentMapper paymentMapper, ILogger<PaymentService> logger, IOrderRepository<Order> orderRepository, IShoppingCartRepository<ShoppingCart> shoppingCartRepository,
            ShoppingCartMapper shoppingCartMapper, IUnitOfWork unitOfWork, IPaymentRepository<Payment> paymentRepository)
        {
            _paymentMapper = paymentMapper;
            _logger = logger;
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartMapper = shoppingCartMapper;
            _unitOfwork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public async Task<Result> AddPaymentService(PaymentDTO p)
        {
            Result result = new Result();
            result.Success = true;
            try
            {
                //Comprobar el usuario ligado a ese pago -desde el order?
                var user = await _orderRepository.IsOrderPlacedByUserAsync(p.IdOrder, p.IdUser);

                if (!user)
                {
                    result.Success = false;
                    result.Error = $"El usuario no se corresponde al pedido seleccionado";
                    _logger.LogError(result.Error.ToString());
                }

                var shoppingCart = await _shoppingCartRepository.GetCartByOrderIdAsync(p.IdOrder);

                var paymentIsTrue = await _shoppingCartRepository.IsOrdenCart(p.IdOrder);

                if (shoppingCart != null)
                {
                    //Mapeo antes de actualizar.
                    _shoppingCartMapper.MapToPaymentCart(shoppingCart);
                    //Actualizar la tabla de shoppingCart como pagado para el IdOrden correcto:
                    await _shoppingCartRepository.ModifyCart(paymentIsTrue);
                }

                //Verificar el order en el cual se va a proceder a realizar el pago.
                var payment = await _paymentMapper.MapToPayment(p, shoppingCart);

                if (payment == null)
                {
                    result.Success = false;
                    result.Error = $"Error al intentar pagar para la orden: {p.IdOrder} para el usuario {p.IdUser}";
                    _logger.LogError(result.Error.ToString());

                    return result;
                }


                await _paymentRepository.AddPaymentAsync(payment);

                await _unitOfwork.SaveChangesAsync();

                await _unitOfwork.CommitAsync();


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = $"Error al pagar";
                _logger.LogError(ex.ToString(), result.Error.ToString());

                await _unitOfwork.RollbackAsync();
            }

            return result;
        }
    }
}
