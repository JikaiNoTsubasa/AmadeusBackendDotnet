using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace ama_back_api.DBModels;

public abstract class AmaEntity
{
    public long Id { get; set; }
    public string? Name { get; set; }
    [ForeignKey(nameof(Status))]
    public long StatusId { get; set; }
    public AmaStatus? Status { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public ICollection<AmaCategory>? Categories { get; set; }
    public string? Icon { get; set; }
}
