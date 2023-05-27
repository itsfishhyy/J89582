using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using J89582.Model;
using J89582.Data;
using J89582.Pages.Admin;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<J89582Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("J89582Context") ?? throw new InvalidOperationException("Connection string 'J89582Context' not found.")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<J89582Context>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
    options.LogoutPath = new PathString("/Index");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy",
    policy => policy.RequireRole("Admin"));
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Menus", "AdminPolicy");
});

var app = builder.Build();

/*using (var scope = app.Services.CreateScope)
{
    var services = scope.ServiceProvider;
}*/
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//else
//{
//    app.UseDeveloperExceptionPage();
//    app.UseMigrationEndPoint();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
