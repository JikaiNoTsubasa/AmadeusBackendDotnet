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
            Name = unit.Name,
            CreationDate = unit.CreationDate,
            Status = unit.Status?.ToDTO()
        };
    }

    public static ResponseUser ToDTO(this AmaUser user)
    {
        return new ResponseUser
        {
            Id = user.Id,
            Login = user.Login,
            DisplayName = user.DisplayName
        };
    }

    public static ResponseStatus ToDTO(this AmaStatus status)
    {
        return new ResponseStatus
        {
            Id = status.Id,
            Name = status.Name,
            Description = status.Description
        };
    }
}
