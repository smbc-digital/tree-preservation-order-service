﻿using System.Diagnostics.CodeAnalysis;
using tree_preservation_order_service.Utils.HealthChecks;
using tree_preservation_order_service.Utils.ServiceCollectionExtensions;
using tree_preservation_order_service.Utils.StorageProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockportGovUK.AspNetCore.Availability;
using StockportGovUK.AspNetCore.Availability.Middleware;
using StockportGovUK.NetStandard.Gateways;
using StockportGovUK.NetStandard.Gateways.Extensions;
using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Gateways.MailingService;

namespace tree_preservation_order_service
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddStorageProvider(Configuration);

            services.AddHttpClient<IVerintServiceGateway, VerintServiceGateway>(Configuration);
            services.AddHttpClient<IMailingServiceGateway, MailingServiceGateway>(Configuration);

            services.AddAvailability();
            services.AddSwagger();
            services.AddHealthChecks()
                    .AddCheck<TestHealthCheck>("TestHealthCheck");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler($"/api/v1/error{(env.IsDevelopment() ? "/local" : string.Empty)}");

            app.UseMiddleware<Availability>();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            
            app.UseHealthChecks("/healthcheck", HealthCheckConfig.Options);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Tree Preservation Order Service API");
            });
        }
    }
}
