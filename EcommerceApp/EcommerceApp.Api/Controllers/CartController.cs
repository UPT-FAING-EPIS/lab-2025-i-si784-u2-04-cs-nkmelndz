using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Api.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones del carrito de compras.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly IShipmentService _shipmentService;
        private readonly IDiscountService _discountService;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="CartController"/>.
        /// </summary>
        /// <param name="cartService">Servicio del carrito de compras.</param>
        /// <param name="paymentService">Servicio de pagos.</param>
        /// <param name="shipmentService">Servicio de envíos.</param>
        /// <param name="discountService">Servicio de descuentos.</param>
        public CartController(
            ICartService cartService,
            IPaymentService paymentService,
            IShipmentService shipmentService,
            IDiscountService discountService)
        {
            _cartService = cartService;
            _paymentService = paymentService;
            _shipmentService = shipmentService;
            _discountService = discountService;
        }

        /// <summary>
        /// Procesa el checkout del carrito aplicando el pago y el envío.
        /// </summary>
        /// <param name="card">Información de la tarjeta de pago.</param>
        /// <param name="addressInfo">Información de envío.</param>
        /// <returns>Mensaje indicando si el cargo fue exitoso.</returns>
        [HttpPost]
        public string CheckOut(ICard card, IAddressInfo addressInfo)
        {
            double total = _cartService.Total();
            double discountedTotal = _discountService.ApplyDiscount(total);

            var result = _paymentService.Charge(discountedTotal, card);
            if (result)
            {
                _shipmentService.Ship(addressInfo, _cartService.Items());
                return "charged";
            }
            else
            {
                return "not charged";
            }
        }
    }
}
