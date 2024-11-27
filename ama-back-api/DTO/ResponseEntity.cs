namespace ama_back_api.DTO;

public record class ResponseEntity
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long StatusId { get; set; }
    public ResponseStatus? Status { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<ResponseCategory>? Categories { get; set; }
    public string? Icon { get; set; }
}
