using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleProduct
{
    public class CancelSaleProductRequest
    {
        /// <summary>
        /// The unique identifier of the sale to be canceled
        /// </summary>
        [Required]
        public Guid SaleId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
    }
}
