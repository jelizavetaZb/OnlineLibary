using LightInject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure;
using OnlineLibary.IoC;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Stores;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLightInject().ConfigureContainer<IServiceContainer>(container =>
{
    container.RegisterFrom<DefaultWebCompositionRoot>();
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddIdentity<User, UserRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddRoleStore<IdentityRoleStore>()
    .AddUserStore<IdentityUserStore>()
    .AddUserManager<IdentityUserManager>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["System:SiteFilesPath"])),
    RequestPath = new PathString("/_SiteFiles")
});

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.UseHttpLogging();
app.MapRazorPages();

app.Run();
