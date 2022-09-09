using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
    options
        .OperationFilter<OptionalPathParameterFilter>());

builder.Services.AddDbContext<DataContext>(options => 
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"))
        .UseLazyLoadingProxies());

#endregion

var app = builder.Build();

#region Http pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion