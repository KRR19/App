using App.BussinesLogicLayer.Helper;
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
using Microsoft.OpenApi.Models;
using Stripe;
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
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));

            services.AddIdentity<User, IdentityRole>
                (options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddTransient<IPrintingEditionService, PrintingEditionService>();
            services.AddTransient<IPrintingEditionsRepository, PrintingEditionsRepository>();

            services.AddTransient<IOrderItemService, OrderItemService>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();

            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();


            services.AddTransient<IAuthorInPrintingEditionsRepository, AuthorInPrintingEditionsRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUrlHelperFactory, UrlHelperFactory>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<PaymentHelper>(Configuration.GetSection("Stripe"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Go",
                    Version = "v1"
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseMiddleware<LogService>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
