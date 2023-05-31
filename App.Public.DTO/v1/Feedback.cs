using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Feedback : DomainEntityId
{
    public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }

    public Guid? ProductId { get; set; }
    // public Product? Product { get; set; }

    public Guid? PcId { get; set; }
    // public Pc? Pc { get; set; }

    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Rating))]
    public int Rating { get; set; }

    [MaxLength(4096)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    public string Description { get; set; } = default!;
}