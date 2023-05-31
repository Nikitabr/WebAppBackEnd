using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class SpecificationType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.SpecificationType), Name = nameof(Title))]
    public string Title { get; set; } = default!;
    
    
    public Guid ProductId { get; set; }
    // public Product? Product { get; set; }
    
    public ICollection<Specification>? Specifications { get; set; }
}