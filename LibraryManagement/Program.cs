using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server connection string
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// Add Identity system
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Register repositories
builder.Services.AddScoped<IBookRepository, BookRepositoryWithSqlServer>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepositoryWithSqlServer>();

var app = builder.Build();

// Seed admin user and role during application startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

    // Create role and admin user if not exist
    await dbContext.Database.MigrateAsync(); // Ensure database is created and up-to-date

    // Create Admin role if it doesn't exist
    var adminRole = await roleManager.FindByNameAsync("Admin");
    if (adminRole == null)
    {
        adminRole = new AppRole { Name = "Admin" };
        await roleManager.CreateAsync(adminRole);
    }

    // Create admin user if it doesn't exist
    var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
    if (adminUser == null)
    {
        adminUser = new AppUser
        {
            UserName = "admin",
            Email = "admin@admin.com"
        };

        var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
        if (result.Succeeded)
        {
            // Assign Admin role to the user
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
