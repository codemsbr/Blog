using Blog.Areas.Admin.Helpers;
using Blog.Contexts;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<BlogDbContexts>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
            }).AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789._";
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<BlogDbContexts>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.LogoutPath = new PathString("/Auth/Logout");
                options.AccessDeniedPath = new PathString("/Home/AccessDenied");

                options.Cookie = new()
                {
                    Name = "IdentityCookie",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.SlidingExpiration = true;
            });

            builder.Services.AddSession();

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

            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            PathConstant.RootPath = builder.Environment.WebRootPath;
            app.Run();
        }
    }
}