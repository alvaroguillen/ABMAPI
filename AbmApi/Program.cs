using AbmApi.Application.Query;
using AbmApi.Infraestructure.Context;
using AbmApi.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var envDirecta = Environment.GetEnvironmentVariable("ApplicationName");


var titleFromEnv = !string.IsNullOrEmpty(envDirecta)
                   ? envDirecta
                   : (builder.Configuration["ApplicationName"] ?? "API Default Name");
// Add services to the container.
builder.Services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductoByIdHandler).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = titleFromEnv,
        Version = "v1"
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.DocumentTitle = titleFromEnv;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
