﻿using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Picturesque.Application.SendGrid;
using Picturesque.DB;
using Picturesque.Domain;
using Picturesque.Services;

namespace PicturesqueAPI
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
            services.AddDbContext<PicturesqueDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("dbconn")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<PicturesqueDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    /*As long as credentials security is used in your application you should specify domain(s) from which CORS requests may come to your server. */
                    .WithOrigins(new string[] {
                        Configuration["AppUrl"]
                        }
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    IdentityModelEventSource.ShowPII = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("is_admin"));
            });

            services.AddScoped<IUserServiceManager, UserServiceManager>();
            services.AddScoped<IGameServiceManager, GameServiceManager>();
            services.AddScoped<ICategoryServiceManager, CategoryServiceManager>();
            services.AddScoped<IPictureServiceManager, PictureServiceManager>();
            services.AddScoped<IStatisticsServiceManager, StatisticsServiceManager>();

            services.AddTransient<IEmailSender, EmailServiceManager>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Category, CategoryView>();
                mc.CreateMap<Picture, PictureView>();
                mc.CreateMap<User, UserView>();
                mc.CreateMap<UserStatistics, UserStatisticsView>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PicturesqueDbContext>();
                DbInitializer.Initialize(context);
            }

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
