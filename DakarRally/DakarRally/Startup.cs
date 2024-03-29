﻿using DakarRally.Infrastructure;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DakarRally
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
            services.AddDbContext<DakarRallyContext>(opt => opt.UseInMemoryDatabase("DakarRallyDb").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IAmRaceService, RaceService>();
            services.AddScoped<IAmRaceRepository, RaceRepository>();

            // register the swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DakarRally API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable Swagger middleware 
            app.UseSwagger();

            // specify the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Dakar Rally API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
