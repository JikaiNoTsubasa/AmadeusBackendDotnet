using System;

namespace ama_back_api.DTO;

public record ResponseUnit : ResponseEntity
{
    public ICollection<ResponseProject>? Projects { get; set; }
}
