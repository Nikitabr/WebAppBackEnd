using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace WebApp.DTO;

public class ServiceDTO : DomainEntityMetaId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }

    [MaxLength(4096)]
    public string Description { get; set; } = default!;
    
    public ICollection<Order>? Orders { get; set; }
}