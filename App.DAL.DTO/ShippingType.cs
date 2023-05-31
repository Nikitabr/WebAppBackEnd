using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.DAL.DTO;

public class ShippingType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ShippingType), Name = nameof(Title))]
    public LangStr Title { get; set; } = new ();
    
    public ICollection<ShippingInfo>? ShippingInfos { get; set; }
}