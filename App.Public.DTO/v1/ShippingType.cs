using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ShippingType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingType), Name = nameof(Title))]
    public string Title { get; set; } = default!;
    
    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
}