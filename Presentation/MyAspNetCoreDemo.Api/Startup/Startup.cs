using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyAspNetCoreDemo.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MyAspNetCoreDemo.Web
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
            //注册服务
            services.AddTransient<ILogin, OracleLogin>();
            services.AddTransient<ILogin, SqlServerLogin>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            string rabbitMqConnection = Configuration["ConnectionStrings:RabbitMqConnection"];
            services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConnection));

            //数据保护
            //https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-2.2
            services.AddDataProtection();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                //显示注释
                foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, "MyAspNetCoreDemo.*.xml"))
                {
                    options.IncludeXmlComments(file);
                }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            //利用EasyNetQ的自动订阅者进行订阅，
            app.UseSubscribe("OrderService", Assembly.GetExecutingAssembly());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
