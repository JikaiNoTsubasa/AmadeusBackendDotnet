using System;
using System.ComponentModel.DataAnnotations;

namespace ama_back_api.DBModels;

public class AmaCategory
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<AmaEntity>? Entities { get; set; }
}
