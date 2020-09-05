using GlobomanticsProductCatalog.Data;
using GlobomanticsProductCatalog.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GlobomanticsProductCatalog
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
            services.AddControllersWithViews();

            // LOCAL CONNECTION STRING
            const string connectionString = @"Server=(localdb)\mssqllocaldb;" +
                "Database=GlobomanticsBookDb;" +
                "Trusted_Connection=True";
            // DEVELOPMENT CONNECTION STRING
            //const string connectionString = @"Server=devsql.globomantics.com;" +
            //    "Database=Globomantics_Dev;" +
            //    "User Id=globomanticsCatalogUser;" +
            //    "Password=58nx761";
            // STAGING CONNECTION STRING
            //const string connectionString = @"Server=stgsql.globomantics.com;" +
            //    "Database=Globomantics_Stg;" +
            //    "User Id=globomanticsCatalogUser;" +
            //    "Password=k8erv91x";
            // PRODUCTION CONNECTION STRING
            //const string connectionString = @"Server=prosql.globomantics.com;" +
            //    "Database=GlobomanticsBook;" +
            //    "User Id=globomanticsCatalogUser;" +
            //    "Password=Pzxy107ba";
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<OrderApiClient>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();
                SeedData.Execute(context);
            }
        }
    }
}
