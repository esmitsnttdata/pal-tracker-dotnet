﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PalTracker;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Steeltoe.Management.CloudFoundry;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint.Info;

namespace PalTracker
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
            services.AddMvc();
            services.AddScoped<ITimeEntryRepository, MySqlTimeEntryRepository>();
            services.AddSingleton(sp => new WelcomeMessage(Configuration.GetValue<string>("WELCOME_MESSAGE", "WELCOME_MESSAGE not configured ")));
            services.AddSingleton<IOperationCounter<TimeEntry>, OperationCounter<TimeEntry>>();
            services.AddCloudFoundryActuators(Configuration);
            services.AddSingleton<IHealthContributor, TimeEntryHealthContributor>();
            services.AddSingleton<IInfoContributor, TimeEntryInfoContributor>();
                services.AddSingleton(sp => new CloudFoundryInfo(
                Configuration.GetValue<string>("PORT", "port not configured "),
                Configuration.GetValue<string>("MEMORY_LIMIT", "memoryLimit not configured "),
                Configuration.GetValue<string>("CF_INSTANCE_INDEX", "cfInstanceIndex not configured "),
                Configuration.GetValue<string>("CF_INSTANCE_ADDR", "cfInstanceAddr not configured ")
            ));
            services.AddDbContext<TimeEntryContext>(options => options.UseMySql(Configuration));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCloudFoundryActuators();
        }
    }
}
