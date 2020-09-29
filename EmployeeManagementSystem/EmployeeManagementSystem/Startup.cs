using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using EmployeeManagementSystem.Hubs;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   var resolver = options.SerializerSettings.ContractResolver;
                   if (resolver != null)
                       (resolver as DefaultContractResolver).NamingStrategy = null;
               });
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MYDb")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddSignalR();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

           // Adding Jwt Bearer  
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = Configuration["JWT:ValidAudience"],
                   ValidIssuer = Configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
               };
           });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/NotificationHub");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
