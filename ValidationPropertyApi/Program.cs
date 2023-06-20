using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ValidationPropertyApi.Controllers.DataAnnotationExample;
using ValidationPropertyApi.Controllers.SwaggerConfig;

var builder = WebApplication.CreateBuilder(args);

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(xmlFilePath);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo { Title = "Data Annotation API", Version = "V1.0" });
    c.SwaggerDoc("V2", new OpenApiInfo { Title = "ModelState API", Version = "V2.0" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "ValidationPropertyApi.xml");
    c.IncludeXmlComments(filePath);
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRouting();
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, req) =>
        {
            swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
        });
    });

    app.UseSwaggerUI(options =>
    {
        
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
            options.DefaultModelsExpandDepth(-1);
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        }
    });
    app.UseEndpoints(endpoints => endpoints.MapControllers());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desc in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "My Test API",
                Version = desc.ApiVersion.ToString(),
            });
        }
    }
}