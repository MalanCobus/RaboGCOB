using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rabobank.TechnicalTest.GCOB.Commands.Handlers.Commands;
using Rabobank.TechnicalTest.GCOB.DataLayer.Repositories;
using Rabobank.TechnicalTest.GCOB.Queries.Handlers.Customers;
using System;
using System.IO;
using System.Reflection;

namespace Rabobank.TechnicalTest.GCOB
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
            services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
            services.AddSingleton<ICountryRepository, InMemoryCountryRepository>();
            services.AddSingleton<IAddressRepository, InMemoryAddressRepository>();

            services.AddControllers();
            ConfigureSwagger(services);
            ConfigureCommandsQueries(services);
            services.AddLogging();
        }

        private void ConfigureCommandsQueries(IServiceCollection services)
        {
            services.AddScoped<CustomerCommandHandler>();
            services.AddScoped<CustomerQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

    }
}
