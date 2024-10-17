namespace ama_back_api.DTO;

public record RequestLogin
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}
