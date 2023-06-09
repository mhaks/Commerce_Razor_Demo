using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommerceRazorDemo.Data;
using CommerceRazorDemo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using CommerceRazorDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CommerceRazorDemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommerceRazorDemoContext") ?? throw new InvalidOperationException("Connection string 'CommerceRazorDemoContext' not found.")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CommerceRazorDemoContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/AccessDenied");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Errors/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.Run();
