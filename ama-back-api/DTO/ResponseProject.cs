namespace ama_back_api.DTO;

public record ResponseProject
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public ResponseStatus? Status { get; set; }
    public DateTime? CreationDate { get; set; }
    public List<ResponseCategory>? Categories { get; set; }
    public string? Description { get; set; }
}
