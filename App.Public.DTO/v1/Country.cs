using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Country : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Country), Name = nameof(CountryName))]
    public string CountryName { get; set; } = default!;
    
    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
}