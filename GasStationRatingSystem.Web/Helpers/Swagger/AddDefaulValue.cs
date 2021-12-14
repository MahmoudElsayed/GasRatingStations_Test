using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http.Description;

namespace GasStationRatingSystem.Web.Helpers.Swagger
{
    public class AddDefaulValue : Swashbuckle.Swagger.IOperationFilter
    {
        public void Apply(Swashbuckle.Swagger.Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null || !operation.parameters.Any())
            {
                return;
            }
            var attributes = apiDescription.GetControllerAndActionAttributes<SwaggerDefaultValueAttribute>().ToList();

            if (!attributes.Any())
            {
                return;
            }

            foreach (var parameter in operation.parameters)
            {
                var attr = attributes.FirstOrDefault(it => it.Parameter == parameter.name);
                if (attr != null)
                {
                    parameter.@default = attr.Value;
                }
            }
        }

      
    }
}
