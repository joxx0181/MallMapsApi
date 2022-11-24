using MallMapsApi;
using MallMapsApi.Data;
using MallMapsApi.Interface;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICrudAcess, DataAcess>();
builder.Services.AddScoped<ITest, DataHandler>();
builder.Services.AddScoped<IVerify, DataHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mall API",
        Description = "MallMapsApi",
        TermsOfService = new Uri("https://TheMall/Contact/"),
        Contact = new OpenApiContact
        {
            Name = "Contact information",
            Url = new Uri("https://TheMall/contact/"),
            Email = "Support@themall.dk",
        },
        License = new OpenApiLicense
        {
            Name = "License"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This lines
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MallMapsApi");
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
