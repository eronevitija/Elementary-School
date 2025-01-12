using ElementarySchool.Data;
using ElementarySchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", builder =>
    {
        builder
        .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader();
        //.AllowCredentials();
    });
});

//Adding services
builder.Services.AddDbContext<ElementarySchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElementarySchoolContext")));

builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddControllers();



//From cursor
// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Elementary School API",
        Version = "v1",
        Description = "API for Elementary School Management System"
    });
});

builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

//from cursor
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elementary School API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowReact");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();



app.Run();  