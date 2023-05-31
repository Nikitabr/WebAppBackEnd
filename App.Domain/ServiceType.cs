using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class ServiceType : DomainEntityMetaId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ServiceType), Name = nameof(Title))]
    public LangStr Title { get; set; } = new ();

    public ICollection<Service>? Services { get; set; }
}