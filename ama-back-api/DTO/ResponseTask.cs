namespace ama_back_api.DTO;

public record ResponseTask : ResponseEntity
{
    public ResponseProject? Project { get; set; }
}
