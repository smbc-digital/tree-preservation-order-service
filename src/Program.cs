﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using StockportGovUK.AspNetCore.Logging.Elasticsearch.Aws;

namespace tree_preservation_order_service
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./Config/appsettings.json", false, true)
            .AddJsonFile($"./Config/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .AddJsonFile("./Config/Secrets/appsettings.secrets.json", false, true)
            .AddJsonFile($"./Config/Secrets/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.secrets.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteToElasticsearchAws(Configuration)
                .CreateLogger();

            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(Configuration);
                })
                .UseSerilog()
                .Build();
    }
}
