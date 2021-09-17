using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API
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
            // just register CatalogDatabaseSettings with DI container so that config can be provided when Options will be invoked
            services.Configure<CatalogDatabaseSettings>(Configuration.GetSection(nameof(CatalogDatabaseSettings)));

            services.AddSingleton<ICatalogDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Configuration.GetSection("CatalogDatabaseSettings:ConnectionString").Value));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
