using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly IShipmentService _shipmentService;
    private readonly IDiscountService _discountService;

    public CartController(
      ICartService cartService,
      IPaymentService paymentService,
      IShipmentService shipmentService,
      IDiscountService discountService
    )
    {
        _cartService = cartService;
        _paymentService = paymentService;
        _shipmentService = shipmentService;
        _discountService = discountService;
    }

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
