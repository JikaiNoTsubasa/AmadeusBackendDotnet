using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ama_back_api.DBModels;

public class AmaTask : AmaEntity
{
    [ForeignKey(nameof(Project))]
    public long ProjectId { get; set; }
    public AmaProject? Project { get; set; }
    public IEnumerable<AmaTask>? SubTasks { get; set; }
    [ForeignKey(nameof(ParentTask))]
    public long ParentTaskId { get; set; }
    public AmaTask? ParentTask { get; set; }
}
