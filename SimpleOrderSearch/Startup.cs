using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using FluentValidation.AspNetCore;
using FluentValidation;
using SimpleOrderSearch.Service.Validators;
using SimpleOrderSearch.Service.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using GraphiQl;
using GraphQL.Types.Relay;
using GraphQL.Relay.Types;
using SimpleOrderSearch.Service.GraphQL.Types;

namespace SimpleOrderSearch
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
            
            services.AddMvc(setupAction: options => 
                            {
                                options.Filters.Add<ValidationFilter>();
                            })
                    .AddFluentValidation(mvcconfig => mvcconfig.RegisterValidatorsFromAssemblyContaining<Startup>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x => 
            {
                x.SwaggerDoc("v1", new Info() { Title = "", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IDataAccessor<OrderInfo>, JsonDataAccessor>();
            services.AddSingleton<AbstractValidator<OrderSearchQuery>, OrderSearchQueryValidator>();
            services.AddTransient(typeof(ConnectionType<>));
            services.AddTransient(typeof(EdgeType<>));
            services.AddTransient<NodeInterface>();
            services.AddTransient<PageInfoType>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // middlewares
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseGraphiQl("/graphql");
            app.UseMvc();
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions); // gets section from json settings file.
            //app.UseSwagger(option =>
            //{
            //    //option.RouteTemplate = ""
            //});

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Orders API");
                //c.RoutePrefix = string.Empty;
            });



        }

        private string GetXmlCommentsPath()
        {
            var app = System.AppContext.BaseDirectory;
            return System.IO.Path.Combine(app, "ASPNETCoreSwaggerDemo.xml");
        }
    }
}
