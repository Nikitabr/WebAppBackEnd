using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class ServiceType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.ServiceType), Name = nameof(Title))]
    public LangStr Title { get; set; } = new ();

    public ICollection<Service>? Services { get; set; }
}