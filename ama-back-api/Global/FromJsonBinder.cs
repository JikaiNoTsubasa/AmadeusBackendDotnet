using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ama_back_api.Global;

public class FromJsonBinder : IModelBinder
{

    protected string _name { get; set; }
    public FromJsonBinder(string name)
    {
        _name = name;
    }
    
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext, nameof(bindingContext));

        JObject bodyObj;

        bool ok = bindingContext.HttpContext.Items.TryGetValue("FromJsonBinder.JsonBodyObj", out var jObject);

        if(!ok || jObject is not JObject)
        {
            var body = bindingContext.HttpContext.Request.Body;
            var bodyText = await new StreamReader(body).ReadToEndAsync();
            
            try
            {
                bodyObj = JObject.Parse(bodyText);
            }
            catch(Newtonsoft.Json.JsonReaderException ex)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelMetadata.ParameterName!, $"Error parsing body: {ex.Message}");

                return;
            }

            // On met le body dans le contexte afin de ne pas devoir réinterprêter le json au prochain bind de la même requête
            bindingContext.HttpContext.Items["FromJsonBinder.JsonBodyObj"] = bodyObj;
        }
        else
        {
            bodyObj = (jObject as JObject)!; // Suite au if la conversion ne peut que marcher
        }


        JToken? token = null;

        foreach(var seg in _name.Split('.'))
        {
            if(token is null)
            {
                token = bodyObj[seg];
            }
            else
            {
                token = token[seg];
            }
        }

        if(token is null)
        {
            // On ne fait pas planter le model binding si la valeur n'existe pas dans le json car elle n'est pas forcement obligatoire
            return;
        }

        var realValue = token.ToObject(bindingContext.ModelType);

        if(realValue is not null && realValue.GetType().IsAssignableTo(bindingContext.ModelType))
        {
            // Si c'est un DateTime, on le convertit en UTC
            if(realValue is DateTime dt)
            {
                realValue = dt.ToUniversalTime();
            }

            bindingContext.Result = ModelBindingResult.Success(realValue);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelMetadata.ParameterName!, $"The type of '{_name}' is not assignable to '{bindingContext.ModelType.Name}'");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return;
    }
}
