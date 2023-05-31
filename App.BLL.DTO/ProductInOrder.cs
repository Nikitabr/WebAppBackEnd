using Base.Domain;

namespace App.BLL.DTO;

public class ProductInOrder : DomainEntityId
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
}