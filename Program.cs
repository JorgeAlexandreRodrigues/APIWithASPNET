using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Business;
using RestWithASPNET.Business.Implementations;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Hypermedia.Enricher;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connection = builder.Configuration["MySQLConnection:DefaultConnection"];
        var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

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
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

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