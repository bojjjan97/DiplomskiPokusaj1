using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Helper
{
    public class SwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (KeyValuePair<string, OpenApiSchema > item in swaggerDoc.Components.Schemas )
            {
                if(item.Key.Contains("Create") || item.Key.Contains("Update"))
                {
                    swaggerDoc.Components.Schemas.Remove(item.Key);
                }
            }
        }
    }
}
