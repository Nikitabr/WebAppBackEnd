using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class SpecificationTypeDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    public ICollection<Specification>? Specifications { get; set; }
}