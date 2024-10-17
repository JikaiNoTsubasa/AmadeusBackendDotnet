namespace ama_back_api.DTO;

public record ResponseStatus
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
