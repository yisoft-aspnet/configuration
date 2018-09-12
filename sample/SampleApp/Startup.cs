//      )                             *
//   ( /(        *   )       (      (  `
//   )\()) (   ` )  /( (     )\     )\))(
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_)
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  |
//  \ V /  | | _ | |  | _|  / _ \  | |\/| |
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_|
//
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.
//
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddYamlFile("appsettings.yml", false)
                .AddYamlFile($"appsettings.{env.EnvironmentName}.yml", true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.Run(async context =>
            {
                var output = Configuration["database:pgsql:default"];

                await context.Response.WriteAsync(output);
            });
        }
    }
}
