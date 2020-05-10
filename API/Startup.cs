using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Serialization;

namespace API
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Configuration = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appSettings.json")
            //    .Build(); //configuration;
    }



        public static IConfiguration Configuration { get; private set; }
        public static string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver
                        = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            string enviorment = "D";//Configuration["Envoirment"];

            if (enviorment == "P")
                ConnectionString = Configuration["ConnectionStrings:prodConnection"];
            else if (enviorment == "U")
                ConnectionString = "Server=twittinest.c3gce2a7fa8f.ap-south-1.rds.amazonaws.com;Database=uat_DB002A;User Id=admin; Password=buzz#2008;";//Configuration["ConnectionStrings:uatConnection"];
            else
                ConnectionString = "Server=NIKHIL\\SQLEXPRESS;Database=DB003;Trusted_Connection=True;";//Configuration["ConnectionStrings:devConnection"];

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");


            app.UseHttpsRedirection();
            app.UseMvc();

        }

    }
}
