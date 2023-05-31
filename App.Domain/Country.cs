using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Country : DomainEntityMetaId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Country), Name = nameof(CountryName))]
    public LangStr CountryName { get; set; } = new ();
    
    public ICollection<ShippingInfo>? ShippingInfos { get; set; }

}