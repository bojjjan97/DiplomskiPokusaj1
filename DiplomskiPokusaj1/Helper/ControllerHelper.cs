using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Helper
{
    public class ControllerHelper
    {
        public static void Errors(IdentityResult result, ModelStateDictionary ModelState)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("Errors", error.Description);
        }
        public static void IncludeContentRange(string entity, int lowerLimit, int upperLimit, int total, HttpRequest context)
        {
            var headerValue = string.Format("{0} {1}-{2}/{3}", entity.ToLower(), lowerLimit, upperLimit, total);
            context.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            context.HttpContext.Response.Headers.Add("Content-Range", headerValue);
        }
    }
}
