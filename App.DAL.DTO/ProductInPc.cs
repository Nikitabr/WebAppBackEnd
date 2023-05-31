using Base.Domain;

namespace App.DAL.DTO;

public class ProductInPc : DomainEntityId
{
    public Guid PcId { get; set; }
    public Pc? Pc { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}