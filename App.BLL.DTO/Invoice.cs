using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class Invoice : DomainEntityId
{
    
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PaymentMethod { get; set; } = default!;

    public string DeliveryMethod { get; set; } = default!;

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    [MinLength(15), MaxLength(15)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Invoice), Name = nameof(Code))]
    public string Code { get; set; } = default!;
}