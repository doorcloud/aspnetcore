using Microsoft.EntityFrameworkCore;
using webapp.src.Data;
using webapp.src.Repositories;
using webapp.src.Services;

namespace webapp.src;
public class Startup(IConfiguration config)
{
    public IConfiguration Config { get; } = config;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        string? connectionString = Config.GetConnectionString("DefaultConnection");
        Console.WriteLine(connectionString);
        services.AddDbContext<LContext>(options => options.UseMySQL("Server=localhost;User=root;Password=mypassword;Database=vendora;TreatTinyAsBoolean=true;"));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddControllers();
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
                // app.UseDeveloperExceptionPage();
        }
        else {
                app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
        }
        app.UseRouting();

        app.UseEndpoints(static endpoints => {
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller}/{action=Index}/{id?}"
                // );
                endpoints.MapControllerRoute(
                    name: "GetProduct",
                    pattern: "api/products/{id?}"
                );

        });
    }
}