using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace CustomAuthorizationTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options =>
            {
                //for products
                options.AddPolicy(Policies.RequireProductsView, policy => policy.RequireClaim("Permission", Permissions.Permissions.Products.View));
                options.AddPolicy(Policies.RequireProductsCreate, policy => policy.RequireClaim("Permission", Permissions.Permissions.Products.Create));
                options.AddPolicy(Policies.RequireProductsUpdate, policy => policy.RequireClaim("Permission", Permissions.Permissions.Products.Edit));
                options.AddPolicy(Policies.RequireProductsDelete, policy => policy.RequireClaim("Permission", Permissions.Permissions.Products.Delete));

                //catagory
                options.AddPolicy(Policies.RequireCatagoryView, policy => policy.RequireClaim("Permission", Permissions.Permissions.Catagory.View));
                options.AddPolicy(Policies.RequireCatagoryCreate, policy => policy.RequireClaim("Permission", Permissions.Permissions.Catagory.Create));
                options.AddPolicy(Policies.RequireCatagoryUpdate, policy => policy.RequireClaim("Permission", Permissions.Permissions.Catagory.Edit));
                options.AddPolicy(Policies.RequireCatagoryDelete, policy => policy.RequireClaim("Permission", Permissions.Permissions.Catagory.Delete));

                //subcatagory
                options.AddPolicy(Policies.RequireSubCatagoryView, policy => policy.RequireClaim("Permission", Permissions.Permissions.SubCatagory.View));
                options.AddPolicy(Policies.RequireSubCatagoryCreate, policy => policy.RequireClaim("Permission", Permissions.Permissions.SubCatagory.Create));
                options.AddPolicy(Policies.RequireSubCatagoryUpdate, policy => policy.RequireClaim("Permission", Permissions.Permissions.SubCatagory.Edit));
                options.AddPolicy(Policies.RequireSubCatagoryDelete, policy => policy.RequireClaim("Permission", Permissions.Permissions.SubCatagory.Delete));       
            });

            //jwt settings
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:Token"]))
                };
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Permission Task -  API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
        {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = "Oauth2",
                            Name   = JwtBearerDefaults.AuthenticationScheme,
                            In     = ParameterLocation.Header
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseMiddleware<PermissionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}