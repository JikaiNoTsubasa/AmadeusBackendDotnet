using System;

namespace ama_back_api.DTO;

public record ResponseUnit
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
