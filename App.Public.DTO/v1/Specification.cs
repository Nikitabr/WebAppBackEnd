using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Specification : DomainEntityId
{
    public string SpecificationName { get; set; } = default!;


    public Guid SpecificationTypeId { get; set; }
    // public SpecificationType? SpecificationType { get; set; }

    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    public string Description { get; set; } = default!;
}