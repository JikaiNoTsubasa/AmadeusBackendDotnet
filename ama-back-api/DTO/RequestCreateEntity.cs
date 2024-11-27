namespace ama_back_api.DTO;

public record class RequestCreateEntity
{
    public string? Name { get; set; }
    public long StatusId { get; set; } = 1;
}
