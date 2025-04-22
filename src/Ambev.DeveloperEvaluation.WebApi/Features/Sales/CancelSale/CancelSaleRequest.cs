using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public class CancelSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to be canceled
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
