using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.Application;
using TaskManagement.Application.Infrastructure;
using TaskManagement.Application.Infrastructure.AutoMapper;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Application.User.Command.SignUpCommand;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Helpers;
using TaskManagement.Persistence;

namespace TaskManagement.Api
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
            services.AddControllers();
            // configure strongly typed setting object
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddDefaultIdentity<AppUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TaskManagementDbContext>();
            //services.AddIdentityServer().AddApiAuthorization<AppUser, TaskManagementDbContext>();
            
            services.AddCors();
            services.AddConfiguredDbContext(Configuration);
            services.AddScoped<ITaskManagementDbContext>(s => s.GetService<TaskManagementDbContext>());
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddMediatR(typeof(SignUpCommand).Assembly);
            services.AddHttpContextAccessor();
            // Add Tenant Information
            services.AddMultiTenant()
                .WithHostStrategy()
                .WithConfigurationStore();

            // Add Validator
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(SignUpCommand).Assembly);
            
            // Add Automapper
            services.AddAutoMapper(new Assembly[] {typeof(AutoMapperProfile).Assembly});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseCors(x =>
                x.SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
            app.UseRouting();
            //use multi tenant middleware
            app.UseMultiTenant();

            app.UseAuthentication();
            // app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}