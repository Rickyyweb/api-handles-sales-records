using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProduct;

public class UpdateSaleProductRequest
{

    [JsonIgnore]
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
