using System;
using ama_back_api.DBModels;
using ama_back_api.Migrations;

namespace ama_back_api.DTO;

public static class DTOHelper
{
    public static ResponseUnit ToDTO(this AmaUnit model)
    {
        return new ResponseUnit
        {
            Id = model.Id,
            Name = model.Name,
            CreationDate = model.CreationDate,
            Status = model.Status?.ToDTO(),
            Icon = model.Icon
        };
    }

    public static ResponseUser ToDTO(this AmaUser model)
    {
        return new ResponseUser
        {
            Id = model.Id,
            Login = model.Login,
            DisplayName = model.DisplayName,
            AvatarPath = model.AvatarPath
        };
    }

    public static ResponseStatus ToDTO(this AmaStatus model)
    {
        return new ResponseStatus
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
    }

    public static ResponseProject ToDTO(this AmaProject model)
    {
        return new()
        {
            Id = model.Id,
            Name = model.Name,
            CreationDate = model.CreationDate,
            Status = model.Status?.ToDTO(),
            Categories = model.Categories?.Select(c=>c.ToDTO()).ToList(),
            Description = model.Description
        };
    }

    public static ResponseCategory ToDTO(this AmaCategory model){
        return new(){
            Id = model.Id,
            Name = model.Name
        };
    }
}
