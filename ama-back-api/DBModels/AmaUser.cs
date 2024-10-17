using System;
using System.ComponentModel.DataAnnotations;

namespace ama_back_api.DBModels;

public class AmaUser
{
    [Key]
    public long Id { get; set; }
    public string? Login { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
    public string? AvatarPath { get; set; }
}
