using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;
namespace Oauth_2._0_v2.Handle.swagger
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/bit_sol/api/v1/sign_in", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Auth Owin 2.0 by BIT SOLUTION(Xác thự api)" },
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            type = "string",
                            name = "client_id",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "grant_type",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "username",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "password",
                            required = true,
                            @in = "formData"
                        },
                    }
                }
            });
        }
    }
}