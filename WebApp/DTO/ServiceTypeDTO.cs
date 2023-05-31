using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class ServiceTypeDTO : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;

    public ICollection<Service>? Services { get; set; }
}