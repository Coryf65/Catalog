using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            // register and inject our settings into our app, This allows us to use our MongoDB settings
            services.AddSingleton<IMongoClient>(ServiceProvider => {
                var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
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
            });
        }
    }
}
