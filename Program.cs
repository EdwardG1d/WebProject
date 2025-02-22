using WebProject.Models;
using Microsoft.EntityFrameworkCore;


    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<ProjectDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    

    var app = builder.Build();  

if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); 
}

app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseRouting(); 

app.UseAuthorization(); 


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Index}/{id?}");

app.Run();