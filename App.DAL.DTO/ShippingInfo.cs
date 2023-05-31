using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class ShippingInfo : DomainEntityId
{
    public Guid ShippingTypeId { get; set; }
    public ShippingType? ShippingType { get; set; }

    public Guid CountryId { get; set; }
    public Country? Country { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(City))]
    public string City { get; set; } = default!;

    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(Address1))]
    public string Address1 { get; set; } = default!;

    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(Address2))]
    public string Address2 { get; set; } = default!;

    [MinLength(5), MaxLength(5)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(PostalCode))]
    public string PostalCode { get; set; } = default!;

    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = default!;

    [MaxLength(128)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingInfo), Name = nameof(MailAddress))]
    public string MailAddress { get; set; } = default!;


    public ICollection<Order>? Orders { get; set; }
}