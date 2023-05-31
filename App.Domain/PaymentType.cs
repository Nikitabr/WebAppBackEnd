using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class PaymentType : DomainEntityMetaId
{
    [MaxLength(128)]
    public string PaymentName { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; }

}