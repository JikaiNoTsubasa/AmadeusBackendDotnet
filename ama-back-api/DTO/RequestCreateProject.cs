namespace ama_back_api.DTO;

public record class RequestCreateProject
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long StatusId { get; set; }
    public DateTime? CreationDate { get; set; }
    public List<ResponseCategory>? Categories { get; set; }
    public string? Description { get; set; }
    public long UnitId { get; set; }
}
