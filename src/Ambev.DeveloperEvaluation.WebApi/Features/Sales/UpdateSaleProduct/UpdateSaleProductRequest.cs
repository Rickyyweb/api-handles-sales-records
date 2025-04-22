using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProduct;

public class UpdateSaleProductRequest
{

    [JsonIgnore]
    public Guid SaleId { get; set; }

    [JsonIgnore]
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
