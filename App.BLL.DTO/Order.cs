using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Order : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid PaymentTypeId { get; set; }
    public PaymentType? PaymentType { get; set; }

    public Guid ShippingInfoId { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }

    public DateTime DateTime { get; set; }

    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Price))]
    public decimal Price { get; set; }

    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    [MaxLength(4096)] public string Description { get; set; } = default!;

    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<ProductInOrder>? ProductInOrders { get; set; }
}