using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ama_back_api.Global;

public class FromJsonBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var httpContextAccessor = context.Services.GetRequiredService<IHttpContextAccessor>();
        
        if(httpContextAccessor.HttpContext is null || context.Metadata.ParameterName is null)
        {
            return null;
        }

        var actionDescriptor = httpContextAccessor.HttpContext.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();

        if(actionDescriptor is null)
        {
            return null;
        }

        var parameterInfos = actionDescriptor.MethodInfo.GetParameters().FirstOrDefault(p => p.Name == context.Metadata.ParameterName);

        if(parameterInfos is null)
        {
            return null;
        }

        var attr = parameterInfos.GetCustomAttributes(typeof(FromJsonAttribute), false).FirstOrDefault() as FromJsonAttribute;

        if(attr is null)
        {
            return null;
        }

        return new FromJsonBinder(attr.Name ?? context.Metadata.ParameterName);
    }
}
