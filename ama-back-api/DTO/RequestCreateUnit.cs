namespace ama_back_api.DTO;

public record RequestCreateUnit
{
    public string? Name { get; set; }
    public long StatusId { get; set; } = 1;
}
