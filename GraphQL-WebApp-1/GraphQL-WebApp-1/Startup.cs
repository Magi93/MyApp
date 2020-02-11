using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLWebApp1.Contracts;
using GraphQLWebApp1.Entities.Context;
using GraphQLWebApp1.GraphQL;
using GraphQLWebApp1.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GraphQLWebApp1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_configuration.GetConnectionString("AppConnectionString:DBLocal")));
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["AppConnectionString:DBLocal"]));
           
            //services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);               
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddGraphQL(o => o.ExposeExceptions = true).AddGraphTypes(ServiceLifetime.Scoped).AddDataLoader(); 
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            // services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            //app.UseMvc();

            //app.UseRouting();
            //app.UseGraphQL<AppSchema>();
            //app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            //app.UseMvc();
         

        }
    }
}
