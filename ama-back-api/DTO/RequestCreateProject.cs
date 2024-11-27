namespace ama_back_api.DTO;

public record class RequestCreateProject : RequestCreateEntity
{
    public List<ResponseCategory>? Categories { get; set; }
    public string? Description { get; set; }
    public long? UnitId { get; set; }
}
