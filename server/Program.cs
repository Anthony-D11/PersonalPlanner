using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using server.Data;

var builder = WebApplication.CreateBuilder(args);
var allowSpecificOrigins = "AllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PersonalPlannerContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"))
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
);
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    var FRONTEND_URL = builder.Configuration["FRONTEND_URL"];
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowSpecificOrigins, policy =>
        {
            policy.WithOrigins(FRONTEND_URL).AllowAnyHeader().AllowAnyMethod();
        });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowAll", policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    app.UseCors(allowSpecificOrigins);
}
else
{
    app.UseCors("AllowAll");
}

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PersonalPlannerContext>();
    db.Database.Migrate(); // applies migrations, creates DB if not exists
}

app.MapControllers();

app.Run();
