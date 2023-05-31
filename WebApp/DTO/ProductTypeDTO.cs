using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace WebApp.DTO;

public class ProductTypeDTO : DomainEntityMetaId
{

    [MaxLength(128)]
    public string Title { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }
}