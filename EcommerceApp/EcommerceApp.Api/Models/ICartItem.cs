namespace EcommerceApp.Api.Models
{
    /// <summary>
    /// Representa un artículo en el carrito de compras.
    /// </summary>
    public interface ICartItem
    {
        /// <summary>
        /// Obtiene o establece el identificador del producto.
        /// </summary>
        string ProductId { get; set; }

        /// <summary>
        /// Obtiene o establece la cantidad del producto.
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Obtiene o establece el precio por unidad.
        /// </summary>
        double Price { get; set; }
    }
}
