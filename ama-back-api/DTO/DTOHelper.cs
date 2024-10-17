using System;
using ama_back_api.DBModels;

namespace ama_back_api.DTO;

public static class DTOHelper
{
    public static ResponseUnit ToDTO(this AmaUnit unit)
    {
        return new ResponseUnit
        {
            Id = unit.Id,
            Name = unit.Name
        };
    }
}
