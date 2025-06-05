namespace EcommerceApp.Api.Models
{
    /// <summary>
    /// Representa la información de dirección del cliente.
    /// </summary>
    public interface IAddressInfo
    {
        /// <summary>
        /// Obtiene o establece la calle.
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección adicional.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Obtiene o establece la ciudad.
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Obtiene o establece el código postal.
        /// </summary>
        string PostalCode { get; set; }

        /// <summary>
        /// Obtiene o establece el número de teléfono.
        /// </summary>
        string PhoneNumber { get; set; }
    }
}
