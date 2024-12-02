using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ama_back_api.Global;

public class CustomMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
{
    public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
    {
        if (context.Key.MetadataKind == ModelMetadataKind.Property){
            context.DisplayMetadata.ConvertEmptyStringToNull = false;
        }
    }
}