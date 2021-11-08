using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDesarrollo.DbModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using StructureMap;

namespace FinalDesarrollo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ctrlalumnosContext>(options => options.UseSqlServer(connectionString));

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ALUMNOS - API",
                    Version = "v1",
                    Description = "API Alumnos, Examen Final.",
                    Contact = new OpenApiContact
                    {
                        Name = "Dannys Caceres",
                        Email = "dcaceresr@miumg.edu.gt",
                        Url = new Uri("https://umg.edu.gt/"),
                    },
                });
            });

            var container = new Container();


            container.Configure(config =>
            {
                DbContextOptions<ctrlalumnosContext> dbContextOptions = new DbContextOptionsBuilder<ctrlalumnosContext>()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .Options;

                config.For<IApplicationDbContextOptions>().Use(_ => new ApplicationDbContextOptions(dbContextOptions))
                .ContainerScoped();
                
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CONTROL ALUMNOS API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("LocalApi", "Api/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
