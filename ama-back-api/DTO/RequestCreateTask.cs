namespace ama_back_api.DTO;

public record RequestCreateTask : RequestCreateEntity
{
    public long? ProjectId { get; set; }
    public long? ParentTaskId { get; set; }
    public string? Content { get; set; }
}
