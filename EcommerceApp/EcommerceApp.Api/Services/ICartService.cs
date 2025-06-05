using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services
{
    /// <summary>
    /// Servicio que gestiona el carrito de compras del usuario.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Calcula el total del valor de los productos en el carrito.
        /// </summary>
        /// <returns>El valor total acumulado.</returns>
        double Total();

        /// <summary>
        /// Devuelve todos los artículos actualmente en el carrito.
        /// </summary>
        /// <returns>Colección de elementos del carrito.</returns>
        IEnumerable<ICartItem> Items();
    }
}
