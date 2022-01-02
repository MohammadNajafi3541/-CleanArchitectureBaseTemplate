using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure.Mongo.DataAccess.DataContext;
using Infrastructure.Mongo.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WebApi.Configures;
using WebApi.Mapps;

namespace WebApi
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
            services.Configure<ContractDatabaseSetting>(Configuration.GetSection(nameof(ContractDatabaseSetting)));
            services.AddSingleton<IcontractDatabaseSetting>(sp => sp.GetRequiredService<IOptions<ContractDatabaseSetting>>().Value);
            services.AddScoped<IContractRepository, ContractRepository>();

            services.AddControllers()
                .AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                //option=>{
                // option.ReturnHttpNotAcceptable=true;
                //}).AddXmlDataContractSerializerFormatters();                
                ;


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();


            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"),new QueryStringApiVersionReader("api-version"));
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
           
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
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. try again later.");
                    });
                });
            }

            MapperExtension.Configuration(app.ApplicationServices.GetService<IMapper>());
          
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(option=> {

                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    option.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                }
            
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
