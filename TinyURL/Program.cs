using TinyURL;
using TinyURL.Files;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        //    var builder = WebApplication.CreateBuilder(args);

        //    // Add services to the container.
        //    builder.Services.AddRazorPages();
        //    builder.Services.AddControllers();
        //    builder.Services.AddSingleton<MongoDb>();
        //    builder.Services.AddSingleton<UrlShorteningService>();

        //    var app = builder.Build();

        //    // Configure the HTTP request pipeline.
        //    if (!app.Environment.IsDevelopment())
        //    {
        //        app.UseExceptionHandler("/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.MapRazorPages();
        //    app.MapControllers();

        //    app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();

        });
}

//using TinyURL;

//namespace YourNamespace
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();

//                });
//    }
//}
