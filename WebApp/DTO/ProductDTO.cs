using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class ProductDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    public decimal Price { get; set; }

    public int Rating { get; set; }

    [MaxLength(4096)]
    public string Description { get; set; } = default!;

    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<ProductInPc>? ProductInPcs { get; set; }
    public ICollection<Specification>? Specifications { get; set; }
}