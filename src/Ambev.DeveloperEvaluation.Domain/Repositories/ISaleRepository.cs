using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IRepository <Sale>
{
    Task CancelAsync(Guid saleId, CancellationToken cancellationToken = default);
}
