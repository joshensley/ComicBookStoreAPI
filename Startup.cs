using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI
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

            // For Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // For Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
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

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<ApplicationUserService>();

            services.AddScoped<ICategoryTypeRepository, CategoryTypeRepository>();
            services.AddScoped<CategoryTypeService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<ProductTypeService>();

            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<ProductsService>();

            services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();
            services.AddScoped<ProductSpecificationService>();

            services.AddScoped<IProductSpecificationValueRepository, ProductSpecificationValueRepository>();
            services.AddScoped<ProductSpecificationValueService>();

            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ProductImageService>();

            services.AddScoped<IProductInventoryUnitRepository, ProductInventoryUnitRepository>();
            services.AddScoped<ProductInventoryUnitService>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<CartService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComicBookStoreAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicBookStoreAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
