﻿using tree_preservation_order_service.Helpers;
using tree_preservation_order_service.Models;
using tree_preservation_order_service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StockportGovUK.NetStandard.Extensions.VerintExtensions.VerintOnlineFormsExtensions;
using StockportGovUK.NetStandard.Extensions.VerintExtensions.VerintOnlineFormsExtensions.ConfirmIntegrationFromExtensions;
using System.Collections.Generic;

namespace tree_preservation_order_service.Utils.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITreePreservationOrderService, TreePreservationOrderService>();
            services.AddTransient<IMailHelper, MailHelper>();

            return services;
        }
        public static IServiceCollection RegisterIOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfirmIntegrationFormOptions>
                (configuration.GetSection(ConfirmIntegrationFormOptions.ConfirmIntegrationForm));

            services.Configure<VerintOptions>
                (configuration.GetSection(VerintOptions.Verint));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tree Preservation Order Service API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Authorization using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }
    }
}
