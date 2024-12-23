namespace ama_back_api.DTO;

public record RequestUpdateTask
{
    public string? Name { get; set; }
    public string? Content { get; set; }
    public long? StatusId { get; set; }
    public long? ProjectId { get; set; }
}
