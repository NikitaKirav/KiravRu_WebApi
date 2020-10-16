using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KiravRu.Interfaces;
using KiravRu.Data.Repository;
using KiravRu.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;

namespace KiravRu
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
            services.Configure<CookiePolicyOptions>(options =>
            {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10000000;
            });

            
            services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("ConnectionKiravRu")));

            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    //.SetIsOriginAllowed(_ => true)
                    );
            }); // добавляем сервисы CORS
            //.WithOrigins("http://localhost:3000", "http://54.93.233.204", "https://54.93.233.204")

            //services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    var authOption = new AuthOptions();
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = authOption.AUDIENCE,
                        ValidIssuer = authOption.ISSUER,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOption.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddControllersWithViews();

            services.AddTransient<IAllArticles, ArticleRepository>();
            services.AddTransient<IArticlesCategory, CategoryRepository>();
            services.AddTransient<IArticlesAccess, ArticlesAccessRepository>();
            services.AddTransient<IHistoryChange, HistoryChangeRepository>();
            services.AddTransient<IConstant, ConstantRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowOrigin");
            //app.UseCors(
            //    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            //);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
