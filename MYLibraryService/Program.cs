using Microsoft.EntityFrameworkCore;
using MYLibraryService.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

//HTTP Kullanmak için.
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options => {
    options.UseSqlServer(builder.Configuration["LibraryDatabase:ConnectionString"]);
    options.EnableSensitiveDataLogging(true); //Logları konsol ekranında göstermek için.
});

var app = builder.Build();

app.UseCors("AllowAnyOrigin");
app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
