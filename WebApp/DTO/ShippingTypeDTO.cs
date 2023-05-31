using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class ShippingTypeDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
}