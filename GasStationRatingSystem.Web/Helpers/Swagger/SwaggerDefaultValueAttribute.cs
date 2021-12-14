using System;

namespace GasStationRatingSystem.Web.Helpers.Swagger
{
    public class SwaggerDefaultValueAttribute : Attribute
    {
        public SwaggerDefaultValueAttribute(string param, string value)
        {
            Parameter = param;
            Value = value;
        }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}
