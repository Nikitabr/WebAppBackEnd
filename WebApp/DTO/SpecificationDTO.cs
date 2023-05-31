using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class SpecificationDTO : DomainEntityMetaId
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid SpecificationTypeId { get; set; }
    public SpecificationType? SpecificationType { get; set; }
    
    [MaxLength(128)]
    public string Description { get; set; } = default!;
}