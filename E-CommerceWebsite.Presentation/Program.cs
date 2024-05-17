using E_CommerceWebsite.Context;
using E_CommerceWebsite.Repositories.HighLevel;
using E_CommerceWebsite.Repository;
using E_CommerceWebsite.Service.categoryService;
using E_CommerceWebsite.Service.ordersService;
using E_CommerceWebsite.Service.productService;
using Goba.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_CommerceWebsite.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            ////////// Add services to >>>>>>>>>>(the container).
            //œ« „⁄‰«Â «‰ ·„« «ÃÏ «Õ «Ã «Ê»Ãﬂ  „‰ «·«‰ —›Ì” ÌÃÌ»·Ï «Ê»Ãﬂ  „‰ «·ﬂ·««” œ« 
            // for Product
            builder.Services.AddScoped<IProductsRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductsService>();
            // for orders
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            //for categories
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            //for customer 
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
   



            // i need to teach him how to make an object from db and making db global
            builder.Services.AddDbContext<ECommerceContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            });







            // Add services to the container.

            builder.Services.AddControllers();

            // help usermanager(services) to be created 
            builder.Services.AddIdentity<IdentityUser, IdentityRole>() // to inject UserManager and RoleManager
                .AddEntityFrameworkStores<ECommerceContext>();     // to inject raposatory related to the manager


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // specify the Authorization to jwt package for asp.net 
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    //For more and specific secuirty i can use below 
                    ValidAudience = builder.Configuration["jwt:audience"],
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["jwt:issuer"],
                    ValidateIssuer = true,
                };
            });


            //for swagger view to can handle authentication

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Define the OAuth2.0 scheme that's in use (i.e., Implicit, Password, Application and AccessCode)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                     },
                        new List<string>()
                    }
                });
             });





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}