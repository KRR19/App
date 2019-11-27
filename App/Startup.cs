using App.BussinesLogicLayer.Middleware;
using App.BussinesLogicLayer.Models.Payments;
using App.BussinesLogicLayer.Services;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System;
using System.Text;
using AccountService = App.BussinesLogicLayer.Services.AccountService;
using OrdersService = App.BussinesLogicLayer.Services.OrdersService;

namespace App
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
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));

            services.AddIdentity<User, IdentityRole>
                (options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.AddTransient<IdentityRoleInitializer>();

            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddTransient<IPrintingEditionService, PrintingEditionService>();
            services.AddTransient<IPrintingEditionsRepository, PrintingEditionsRepository>();

            services.AddTransient<ICoverRepository, CoverRepository>();

            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();

            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthorInPrintingEditionsRepository, AuthorInPrintingEditionsRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUrlHelperFactory, UrlHelperFactory>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<PaymentModel>(Configuration.GetSection("Stripe"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Controllers",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });

            string key = Configuration.GetSection("JWT")["SecretKey"];
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            string jwtSchemeName = Configuration.GetSection("JWT")["JwtSchemeName"];
            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtSchemeName;
                options.DefaultChallengeScheme = jwtSchemeName;
            })
            .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("JWT")["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("JWT")["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityRoleInitializer identityRoleInitializer)
        {

            

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

            app.UseMiddleware<LogMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            identityRoleInitializer.SeedRoles();
        }
    }
}
