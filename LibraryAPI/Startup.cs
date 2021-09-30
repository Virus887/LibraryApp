using System;
using System.Text.Json.Serialization;
using LibraryAPI.BookPricesProvider;
using LibraryAPI.Database;
using LibraryAPI.Database.Repositories.Implementations;
using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Services.Implementations;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryAPI
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
            services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddHttpClient("bookPriceAPI", c =>
                    { c.BaseAddress = new Uri("http://60c35511917002001739e94a.mockapi.io/api/v1/"); });

            // configuring DatabaseContext:
            services.AddDbContext<LibraryDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("LibraryDB")));


            // adding services
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookPriceProvider, BookPriceProvider>();

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
