using MongoDB.Driver;

using TinyURL.Files;

namespace TinyURL;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(Configuration.GetConnectionString("MongoDB")));
        services.AddSingleton(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase("TinyUrlDB");
        });

        services.AddRazorPages();
        services.AddControllers();
        services.AddSingleton<MongoDb>();
        services.AddSingleton<UrlShorteningService>();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }
}
