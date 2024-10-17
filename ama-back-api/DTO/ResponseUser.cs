namespace ama_back_api.DTO;

public record ResponseUser
{
    public long Id { get; set; }
    public string? Login { get; set; }
    public string? DisplayName { get; set; }
}
