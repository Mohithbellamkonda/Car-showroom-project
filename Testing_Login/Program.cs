using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Testing_Login;
using Testing_Login.Areas.Identity.Data;
using Testing_Login.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Testing_LoginContextConnection") ?? throw new InvalidOperationException("Connection string 'Testing_LoginContextConnection' not found.");

builder.Services.AddDbContext<Testing_LoginContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Testing_LoginUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Testing_LoginContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MYDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Testing_LoginContextConnection"));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
