using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Service : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }

    [MaxLength(4096)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    public LangStr Description { get; set; } = new ();
    
    public ICollection<Order>? Orders { get; set; }
}