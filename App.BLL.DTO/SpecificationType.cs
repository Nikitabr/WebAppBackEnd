﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class SpecificationType : DomainEntityId
{
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.SpecificationType), Name = nameof(Title))]
    public LangStr Title { get; set; } = new ();
    
    
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public ICollection<Specification>? Specifications { get; set; }
}