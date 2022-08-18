using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KiravRu.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using KiravRu.DAL;
using KiravRu.DAL.Repository.Users;
using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Interface.Categories;
using KiravRu.DAL.Repository.Notes;
using KiravRu.Logic.Interface.Notes;
using KiravRu.Logic.Interface.HistoryChanges;
using KiravRu.Logic.Interface.Constants;
using MediatR;
using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Queries.Users;
using KiravRu.Logic.Mediator.QueryHandlers.Users;
using KiravRu.Middleware;

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

            
            services.AddDbContext<Context>(options =>
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

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Context>();

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

            #region Repositories
            services.AddScoped<IDbContext, Context>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICategoryRepository, DAL.Repository.Categories.CategoryRepository>();
            services.AddTransient<IConstantRepository, DAL.Repository.Constants.ConstantRepository>();
            services.AddTransient<IHistoryChangeRepository, DAL.Repository.HistoryChanges.HistoryChangeRepository>();
            services.AddTransient<INoteAccessRepository, NoteAccessRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddMediatR(typeof(GetUserQuery).Assembly, typeof(GetUserQueryHandler).Assembly);

            try
            {
                var notificationMetadata =
                    Configuration.GetSection("NotificationMetadata").
                    Get<NotificationMetadata>();
                services.AddSingleton(notificationMetadata);
            } catch (Exception ex)
            {
                Program.Logger.Error(ex.Message);
            }
            services.AddControllers();
            //services.AddDataProtection()
            //.PersistKeysToFileSystem(new DirectoryInfo(@"\Keys\"))
            //.SetApplicationName("KiravRu")
            //.SetDefaultKeyLifetime(TimeSpan.FromDays(90));
            //.ProtectKeysWithCertificate(new X509Certificate2("certificate.pfx", "password"));
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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
