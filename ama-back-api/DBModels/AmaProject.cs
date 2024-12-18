using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ama_back_api.DBModels;

public class AmaProject : AmaEntity
{
    public string? Description { get; set; }
    [ForeignKey(nameof(Unit))]
    public long UnitId { get; set; }
    public AmaUnit? Unit { get; set; }
    public ICollection<AmaTask>? Tasks { get; set; }

    [NotMapped]
    public float Progress { get; set; }
}
