using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment(PaymentDTO p)
        {
            Result result = new Result();

            try
            {
                result = await _paymentService.AddPaymentService(p);

                if (!result.Success)
                {
                    return StatusCode(500, result.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

            return Ok(result);
        }
    }
}
