using System;

namespace ama_back_api.DTO;

public record RequestCreateUser
{
    public string? Login { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
}
