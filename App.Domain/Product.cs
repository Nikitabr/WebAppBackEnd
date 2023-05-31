using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Product : DomainEntityMetaId
{
    [MaxLength(128)] 
    [Display(ResourceType = typeof(App.Resources.App.Domain.Product), Name = nameof(Title))]
    public string Title { get; set; } = default!;
    
    public Guid ProductTypeId { get; set; } 
    public ProductType? ProductType { get; set; }

    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Price))]
    public decimal Price { get; set; }

    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Rating))]
    public int Rating { get; set; }

    public int Quantity { get; set; }

    public string Picture { get; set; } = default!;


    [MaxLength(4096)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Common), Name = nameof(Description))]
    public LangStr Description { get; set; } = default!;

    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<ProductInPc>? ProductInPcs { get; set; }
    public ICollection<SpecificationType>? SpecificationTypes { get; set; }

    public ICollection<ProductInOrder>? ProductInOrders { get; set; }

}