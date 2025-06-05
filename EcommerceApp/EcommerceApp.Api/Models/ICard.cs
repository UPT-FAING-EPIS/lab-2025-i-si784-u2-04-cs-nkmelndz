namespace EcommerceApp.Api.Models
{
    /// <summary>
    /// Representa los datos de una tarjeta de crédito o débito.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Obtiene o establece el número de la tarjeta.
        /// </summary>
        string CardNumber { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del titular de la tarjeta.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de vencimiento de la tarjeta.
        /// </summary>
        DateTime ValidTo { get; set; }
    }
}
