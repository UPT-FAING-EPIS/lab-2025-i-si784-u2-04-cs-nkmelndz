using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services
{
    /// <summary>
    /// Servicio encargado del envío de productos al cliente.
    /// </summary>
    public interface IShipmentService
    {
        /// <summary>
        /// Realiza el envío de los productos a la dirección proporcionada.
        /// </summary>
        /// <param name="info">Información de la dirección del cliente.</param>
        /// <param name="items">Colección de productos a enviar.</param>
        void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
    }
}
