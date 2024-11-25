using System;

namespace ama_back_api.DBModels;

public class AmaUnit : AmaEntity
{
    public ICollection<AmaProject>? Projects { get; set; }
}
