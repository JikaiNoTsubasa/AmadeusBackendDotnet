using System;

namespace ama_back_api.DTO;

public record ResponseUnit
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public ResponseStatus? Status { get; set; }
    public DateTime? CreationDate { get; set; }
}
