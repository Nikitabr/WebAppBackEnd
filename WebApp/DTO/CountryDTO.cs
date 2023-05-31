using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class CountryDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string CountryName { get; set; } = default!;

    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
}