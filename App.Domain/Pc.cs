using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Pc : DomainEntityMetaId
{

    [MaxLength(128)] 
    [Display(ResourceType = typeof(App.Resources.App.Domain.Pc), Name = nameof(Title))]
    public string Title { get; set; } = default!;
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Price))]
    public decimal Price { get; set; }
    
    [MaxLength(4096)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    public LangStr Description { get; set; } = new ();

    public ICollection<Order>? Orders { get; set; }
    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<ProductInPc>? ProductInPcs { get; set; }
}