using Flashcards.Domain.Repositories.Cards;
using Flashcards.Domain.Repositories.Decks;
using Flashcards.Domain.Services.Cards;
using Flashcards.Domain.Services.Decks;
using Flashcards.WebAPI.Data;
using Flashcards.WebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Flashcards.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();

            services.AddControllers();

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(Configuration.GetSection("Origins:Allowed").Get<string[]>())
                    .WithMethods(HttpMethods.Get, HttpMethods.Post);
            }));

            services.AddSwaggerDocument();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddSingleton<ICardsRepository, MongoCardsRepository>();
            services.AddSingleton<IDecksRepository, MongoDecksRepository>();

            services.AddSingleton<ICardsService, CardsService>();
            services.AddSingleton<IDecksService, DecksService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            var db = new MongoClient(Configuration["MongoDB:URI"]).GetDatabase(Configuration["MongoDB:DB_Name"]);
            services.AddSingleton(db);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
		endpoints.MapFallbackToFile("/index.html");
            });
	}
    }
}
