using System;

namespace ama_back_api.DBModels;

public class AmaUnit : AmaEntity
{
    public IEnumerable<AmaProject>? Projects { get; set; }
}
