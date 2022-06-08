using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenApiWeb
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions( IApiVersionDescriptionProvider provider ) => this._provider = provider;

        /// <inheritdoc />
        public void Configure( SwaggerGenOptions options )
        {
            // integrate xml comments
            options.IncludeXmlComments(GetXmlCommentsPath(), true);    //TODO: demo to show xml comments for API calls on Swagger UI

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            //TODO: remove from swagger UI & json
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            //qualifies the name of schema objects
            options.CustomSchemaIds((type) => type.FullName);

            //reduces json size by referencing the parent
            options.UseAllOfForInheritance();

            //Polymorphism
            options.UseOneOfForPolymorphism();
            options.SelectDiscriminatorNameUsing((baseType) => baseType.FullName);
            options.SelectDiscriminatorValueUsing((subType) => subType.FullName);
            
        }

        static OpenApiInfo CreateInfoForApiVersion( ApiVersionDescription description )
        {
            var info = new OpenApiInfo()
            {
                Title = "OpenAPIWeb Demo",
                Version = description.ApiVersion.ToString(),
                Description = "Application to showcase OpenAPI Spec",
                Contact = new OpenApiContact { Email = "CommissaryOnline@keefegroup.com", Name = "COL Team", Url= new Uri("https://www.keefegroup.com/home/contact-us-105") },
                TermsOfService = new Uri("https://www.keefegroup.com/home/terms-conditions-108"),
                License = new OpenApiLicense() { Name = "License Keefe Group", Url = new Uri("https://www.keefegroup.com/") }
            };

            if ( description.IsDeprecated )
            {
                info.Description += " This API version has been deprecated.";
            }

            if(description.ApiVersion.MajorVersion < 2)
            {
                info.Description += " Please consider using later version for new development.";
            }
            return info;
        }

        private string GetXmlCommentsPath()
        {
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFileName);
        }
    }
}