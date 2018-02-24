using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NorthwindCore.Data.Domain;

namespace NorthwindCore.Web
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
            services.AddMvc();

            // inject the Northwind Db Context into the app using the Configuration instance
            //  Default: appsettings.json/ConnectionStrings/NorthwindConnection = "Data Source=R2D2;Initial Catalog=Northwind;Integrated Security=False;User ID=sa;Password=lexie07;"
            //  environment var: key=ConnectionStrings:NorthwindConnection value=Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;
            //      environ variables override appsettings following .NET config conventions
            string connStr = Configuration.GetConnectionString("NorthwindConnection");
            services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(connStr)
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
