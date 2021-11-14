using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Register Services here
        public void ConfigureServices(IServiceCollection services)
        {
            // telling mongodb anytome it see a guid to make it a string
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            // same for DateTimeOffset, set as a string
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            // register and inject our settings into our app, This allows us to use our MongoDB settings
            services.AddSingleton<IMongoClient>(ServiceProvider => {
                return new MongoClient(mongoDbSettings.ConnectionString);
            });

            // registering the service, or handle the DI
            // Now I will switch from the in memory object to MongoDB
            //services.AddSingleton<IItemsRepository, InMemItemsRepository>(); // in-memory object list
            services.AddSingleton<IItemsRepository, MongoDbItemsRepository>(); // MongoDB database

            services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false;});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
            });
            
            // adds the services
            // also now using the new MongoDBHealth package, called 'mongodb', and a timeout of 5 seconds
            // "ready" groups items
            services.AddHealthChecks()
                .AddMongoDb(
                    mongoDbSettings.ConnectionString
                    ,name: "mongodb"
                    , timeout: TimeSpan.FromSeconds(5)
                    ,tags: new[] { "ready" }
                ); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware, things that run before the controllers execute
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Enable Health Checks'Route' /status
                // middleware
                // Database is ready to serve requests
                endpoints.MapHealthChecks(
                    "/status/ready"
                    ,new HealthCheckOptions { 
                        Predicate = (check) => check.Tags.Contains("ready"),
                        ResponseWriter = async(context, report) =>                             
                        { 
                            var result = JsonSerializer.Serialize( 
                                new { 
                                    status = report.Status.ToString(),
                                    checks = report.Entries.Select(entry => new {
                                        name = entry.Key,
                                        status = entry.Value.Status.ToString(),
                                        exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                                        duration = entry.Value.Duration.ToString()
                                    })
                                }
                            );
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        }
                    }
                );

                // as long as the rest api, our service, is alive
                endpoints.MapHealthChecks(
                    "/status/live"
                    ,new HealthCheckOptions{ Predicate = (_) => false }
                );
            });
        }
    }
}
