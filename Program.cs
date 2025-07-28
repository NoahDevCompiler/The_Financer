using FinanceTool.Components;
using FinanceTool.Data;
using FinanceTool.Data.Services;
using FinanceTool.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<SavingsService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<LogoMatcher>();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/login";      
    options.LogoutPath = "/logout";   
    options.AccessDeniedPath = "/denied";
});



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();                     
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();                

app.MapRazorPages();                
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();