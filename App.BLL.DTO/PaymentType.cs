using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class PaymentType : DomainEntityId
{
    [MaxLength(128)]
    public string PaymentName { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; }

}