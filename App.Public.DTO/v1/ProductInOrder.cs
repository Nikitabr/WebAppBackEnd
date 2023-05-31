using Base.Domain;

namespace App.Public.DTO.v1;

public class ProductInOrder : DomainEntityId
{
    public Guid ProductId { get; set; }
    // public Product? Product { get; set; }

    public Guid OrderId { get; set; }
    // public Order? Order { get; set; }
}