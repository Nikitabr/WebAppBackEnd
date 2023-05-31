using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ServiceType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ServiceType), Name = nameof(Title))]
    public string Title { get; set; } = default!;

    public ICollection<Service>? Services { get; set; }
}