using DinoManga.Data;
using DinoManga.Map;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DinoManga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                            .AddOData(x => x.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                   };
               });

            builder.Services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
            });

            builder.Services.AddDbContext<MangaDbContext>(
                oprions => oprions.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"))
            );

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            // Thêm cấu hình Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Đặt thời gian timeout cho Session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseODataBatching();
            app.UseCors("CORSPolicy");

            app.UseSession(); // Thêm middleware Session vào pipeline
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
