using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ama_back_api.DBModels;

public class AmaEntity
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    [ForeignKey(nameof(Status))]
    public long StatusId { get; set; }
    public AmaStatus? Status { get; set; }
    public IEnumerable<AmaCategory>? Categories { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
