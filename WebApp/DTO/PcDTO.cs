using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class PcDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    public decimal Price { get; set; }
    
    [MaxLength(4096)]
    public string Description { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; }
    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<ProductInPc>? ProductInPcs { get; set; }
}