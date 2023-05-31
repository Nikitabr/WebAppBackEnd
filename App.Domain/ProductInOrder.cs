using Base.Domain;

namespace App.Domain;

public class ProductInOrder : DomainEntityMetaId
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
}