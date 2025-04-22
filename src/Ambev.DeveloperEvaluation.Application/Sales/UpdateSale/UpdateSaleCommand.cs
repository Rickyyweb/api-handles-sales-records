using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating sale information
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public UpdateSaleCommand(Guid customerId, string customerName, Guid branchId, string branchName, Guid id)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        Id = id;
    }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public Guid Id { get; set; }
    
}
