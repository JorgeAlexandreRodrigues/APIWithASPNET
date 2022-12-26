using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Business;
using RestWithASPNET.Business.Implementations;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;
using RestWithASPNET.Services;
using RestWithASPNET.Services.Implementations;
using RestWithASPNET.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var tokenConfigurations = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
            };
        });

        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });

        var connection = builder.Configuration["MySQLConnection:DefaultConnection"];
        var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
        // Add services to the container.

        builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));
        builder.Services.AddControllers();
        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
        builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        builder.Services.AddApiVersioning();
        
        builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 31))));
        builder.Services.AddMvc(options =>
        {
            options.RespectBrowserAcceptHeader = true;
            options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        }).AddXmlSerializerFormatters();

        builder.Services.AddSingleton(filterOptions);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
            Title = "REST API From 0 to Azure With ASP.NET CORE 6 and Docker",
            Version = "V1",
            Description = "API RESTful developed in course REST API From 0 to Azure With ASP.NET CORE 6 and Docker",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name= "Jorge Alexandre Rodrigues",
                Url = new Uri ("https://github.com/JorgeAlexandreRodrigues")
            }
            });
        } );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API RESTful developed in course REST API From 0 to Azure With ASP.NET CORE 6 and Docker - V1");
            });
        }
        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
        });

        app.Run();
    }
}