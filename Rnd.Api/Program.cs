using Rnd.Api;
using Rnd.Data;

#region Services

var builder = WebApplication.CreateBuilder(args);

Setup.Builder = builder;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(Setup.Swagger);

builder.Services.AddDbContext<DataContext>(Setup.DataContext);

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