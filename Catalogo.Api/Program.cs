using Catalogo.Api;
using Catalogo.Api.Dominio.Base;
using Catalogo.API.Consumers;
using Catalogo.Infra.Context;
using Catalogo.Infra.Logger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region DI

builder.Services.AddCorrelationIdGenerator();

builder.Services.AddHostedService<PaymentProcessedConsumer>();

builder.Services.AddTransient(typeof(BaseLogger<>));

#endregion

#region Banco

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Default"));
});

#endregion


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    SeedData.Seed(db);
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API v1");

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
