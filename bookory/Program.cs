    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using bookory.Data;


    
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<BooksDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("BookConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<BooksDbContext>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    

    app.MapRazorPages();

    app.Run();
