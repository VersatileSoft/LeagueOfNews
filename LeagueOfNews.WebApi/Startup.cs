using Autofac;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceIterator;

namespace LeagueOfNews.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.HttpMethod}");
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "League of news web api", Version = "v1" });
            });

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("wwwroot/credential.json"),
            });

            services.AddControllers();

            services.AddOptions();
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddSingleton<NewPostsService>();
        }

        public void ConfigureContainer(ContainerBuilder builder) => builder.RegisterModule<ServiceModule>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseServicesIterator();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "My serial list web api"));
        }
    }
}