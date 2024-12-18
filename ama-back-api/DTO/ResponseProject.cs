namespace ama_back_api.DTO;

public record ResponseProject : ResponseEntity
{
    public string? Description { get; set; }
    public List<ResponseTask>? Tasks { get; set; }
}
