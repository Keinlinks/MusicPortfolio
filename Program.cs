using Microsoft.EntityFrameworkCore;
using PortafolioBack.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqliteDB>(option => option.UseSqlite(
    builder.Configuration.GetConnectionString("localDb"))
    );
builder.Services.AddCors(options =>
{
    options.AddPolicy("Politicy", app =>
    {
        app.AllowAnyMethod();
        app.AllowAnyHeader();
        app.AllowAnyOrigin();
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});


app.UseRouting();

app.UseCors("Politicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
