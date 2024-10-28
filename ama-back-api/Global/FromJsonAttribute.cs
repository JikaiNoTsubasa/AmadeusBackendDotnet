using System;

namespace ama_back_api.Global;

public class FromJsonAttribute : Attribute
{
    public string? Name { get; set; }
    public FromJsonAttribute() {}

    public FromJsonAttribute(string name)
    {
        Name = name;
    }
}
