//using Microsoft.EntityFrameworkCore;
using ElementarySchool.Models;
using ElementarySchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ElementarySchool.Data;



var builder = WebApplication.CreateBuilder(args);
//using Microsoft.EntityFrameworkCore;
builder.Services.AddDbContext<ElementarySchoolContext>(options =>
//using Microsoft.EntityFrameworkCore;
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElementarySchoolContext") ?? throw new InvalidOperationException("Connection string 'ElementarySchoolContext' not found.")));

var connString = builder.Configuration.GetConnectionString("ElementarySchoolContext");

builder.Services.AddScoped<DatabaseConnection>(provider => new DatabaseConnection(connString));
builder.Services.AddScoped<StudentService>();

builder.Services.AddControllersWithViews();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();