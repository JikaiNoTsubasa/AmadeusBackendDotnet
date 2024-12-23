namespace ama_back_api.DTO;

public record RequestCreateTaskForm
{
    public string? Name { get; set; }
    public long? ProjectId { get; set; }
    public string? Content { get; set; }
    public long StatusId { get; set; } = 1;
}
