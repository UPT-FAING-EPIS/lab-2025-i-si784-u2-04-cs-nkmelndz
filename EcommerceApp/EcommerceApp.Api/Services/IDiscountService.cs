namespace EcommerceApp.Api.Services
{
    /// <summary>
    /// Servicio encargado de aplicar descuentos a los totales del carrito.
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Aplica un descuento al monto total especificado.
        /// </summary>
        /// <param name="totalAmount">Monto original sin descuento.</param>
        /// <returns>Monto con el descuento aplicado.</returns>
        double ApplyDiscount(double totalAmount);
    }
}
