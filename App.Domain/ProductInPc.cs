using Base.Domain;

namespace App.Domain;

public class ProductInPc : DomainEntityMetaId
{
    public Guid PcId { get; set; }
    public Pc? Pc { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}