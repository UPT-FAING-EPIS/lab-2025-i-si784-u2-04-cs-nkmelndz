using EcommerceApp.Api.Controllers;
using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Moq;
using NUnit.Framework;

namespace EcommerceApp.Tests;

public class ControllerTests
{
    private CartController controller;
    private Mock<IPaymentService> paymentServiceMock;
    private Mock<ICartService> cartServiceMock;
    private Mock<IShipmentService> shipmentServiceMock;
    private Mock<IDiscountService> discountServiceMock;
    private Mock<ICard> cardMock;
    private Mock<IAddressInfo> addressInfoMock;
    private List<ICartItem> items;

    [SetUp]
    public void Setup()
    {
        cartServiceMock = new Mock<ICartService>();
        paymentServiceMock = new Mock<IPaymentService>();
        shipmentServiceMock = new Mock<IShipmentService>();
        discountServiceMock = new Mock<IDiscountService>();

        cardMock = new Mock<ICard>();
        addressInfoMock = new Mock<IAddressInfo>();

        var cartItemMock = new Mock<ICartItem>();
        cartItemMock.Setup(item => item.Price).Returns(10);

        items = new List<ICartItem> { cartItemMock.Object };

        cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());
        cartServiceMock.Setup(c => c.Total()).Returns(10);

        controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object, discountServiceMock.Object);
    }

    [Test]
    public void ShouldReturnCharged()
    {
        string expected = "charged";
        discountServiceMock.Setup(d => d.ApplyDiscount(10)).Returns(8); // Ejemplo: 20% de descuento
        paymentServiceMock.Setup(p => p.Charge(8, cardMock.Object)).Returns(true);

        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ShouldReturnNotCharged()
    {
        string expected = "not charged";
        discountServiceMock.Setup(d => d.ApplyDiscount(10)).Returns(8);
        paymentServiceMock.Setup(p => p.Charge(8, cardMock.Object)).Returns(false);

        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        shipmentServiceMock.Verify(s => s.Ship(It.IsAny<IAddressInfo>(), It.IsAny<IEnumerable<ICartItem>>()), Times.Never);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(10, 2, true, "charged")]
    [TestCase(10, 5, false, "not charged")]
    [TestCase(10, 10, true, "charged")]
    public void CheckOut_WithDiscounts_ReturnsExpectedResult(double total, double discount, bool chargeSuccess, string expectedResult)
    {
        cartServiceMock.Setup(c => c.Total()).Returns(total);
        discountServiceMock.Setup(d => d.ApplyDiscount(total)).Returns(discount);
        paymentServiceMock.Setup(p => p.Charge(discount, cardMock.Object)).Returns(chargeSuccess);

        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        if (chargeSuccess)
        {
            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once);
        }
        else
        {
            shipmentServiceMock.Verify(s => s.Ship(It.IsAny<IAddressInfo>(), It.IsAny<IEnumerable<ICartItem>>()), Times.Never);
        }

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
