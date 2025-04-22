using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{

    // Construtor principal de domínio
    public Sale(Guid customerId, string customerName, Guid branchId, string branchName)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        CustomerId = customerId;
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        BranchId = branchId;
        BranchName = branchName ?? throw new ArgumentNullException(nameof(branchName));
    }

    // Data e hora da venda
    public DateTime Date { get; private set; }

    // Cliente (ID e nome desnormalizado)
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; }

    // Filial (ID e nome desnormalizado)
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; }

    // Lista de itens da venda
    public ICollection<SaleProduct> SaleProducts { get; private set; } = [];

    // Status da venda

    // Valor total da venda (calculado)
    public decimal TotalAmount => SaleProducts.Sum(i => i.TotalAmount);

    // Método para adicionar um item à venda
    public void AddItem(SaleProduct saleProduct)
    {
        if (saleProduct == null) throw new ArgumentNullException(nameof(saleProduct));
        SaleProducts.Add(saleProduct);
    }

    // Cancela toda a venda
    public void Cancel()
    {
        if (Status == Status.Inactive)
            throw new InvalidOperationException("Sale is already cancelled");

        Status = Status.Inactive;
    }

    // Cancela um item específico da venda
    public void CancelItem(Guid productId)
    {
        var item = SaleProducts.FirstOrDefault(i => i.Id == productId)
            ?? throw new InvalidOperationException("Item not found in sale");

        item.Cancel();
    }

    public static Sale Create(
        Guid customerId,
        string customerName,
        Guid branchId,
        string branchName,
        List<SaleProduct>? products = null)
    {
        var sale = new Sale(customerId, customerName, branchId, branchName);

        if (products != null)
        {
            foreach (var item in products)
                sale.SaleProducts.Add(item);
        }

        return sale;
    }

    // Método para atualizar informações básicas da venda
    public void Update(Guid customerId, string customerName, Guid branchId, string branchName)
    {
        if (Status != Status.Active)
            throw new InvalidOperationException("Only active sales can be updated");

        CustomerId = customerId;
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        BranchId = branchId;
        BranchName = branchName ?? throw new ArgumentNullException(nameof(branchName));
    }
}
