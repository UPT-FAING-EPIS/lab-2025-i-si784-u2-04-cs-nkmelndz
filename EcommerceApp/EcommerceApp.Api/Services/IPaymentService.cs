using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services
{
    /// <summary>
    /// Servicio que procesa el pago de una compra.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Realiza el cargo del monto especificado usando la tarjeta proporcionada.
        /// </summary>
        /// <param name="total">Monto total a cobrar.</param>
        /// <param name="card">Información de la tarjeta.</param>
        /// <returns><c>true</c> si el cargo fue exitoso; de lo contrario, <c>false</c>.</returns>
        bool Charge(double total, ICard card);
    }
}
