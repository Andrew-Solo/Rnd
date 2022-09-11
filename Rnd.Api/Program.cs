using Dice;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Data;
using Rnd.Api.Swagger;

#region Services

var builder = WebApplication.CreateBuilder(args);

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


#region Http pipeline

var app = builder.Build();

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