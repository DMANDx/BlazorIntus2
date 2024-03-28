using BlazorIntus2.Server.Interfaces;
using BlazorIntus2.Server.Services;
using BlazorIntus2.Shared.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace BlazorIntus2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServerSideBlazor();
            builder.Services.AddBlazorise(options => { options.Immediate = true; }).AddBootstrapProviders().AddFontAwesomeIcons();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IOrder, OrderManager>();

			{
                string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
                //builder.Services.AddLogging(builder => builder.AddConsole().AddDebug());
                //builder.Services.AddLogging(builder =>
                //{
                //    builder.AddConsole();
                //});
            }            

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
                   
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();            
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.MapControllers();
            app.UseBlazorFrameworkFiles();
            app.MapFallbackToFile("index.html");
            app.Run();
        }
    }
}