using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Data.Tables;
using Azure.Identity;
using Azure.Storage.Queues;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using WishingWell.Api.Application;
using WishingWell.Options;
using WishingWell.Repositories;
using WishingWell.Services;

namespace WishingWell.Api
{
    public class Startup
    {
        private readonly ApplicationSettings applicationSettings;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.applicationSettings = this.Configuration.Get<ApplicationSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This swaps authentication from identity server to use unvalidated JWTs (and anonymous if we don't pass a JWT.)
            // This should be used for integration tests, and local testing, but not in production EVER.
            if (this.applicationSettings.DONOTUSE_ONLY_FOR_CI_DISABLE_AUTHENTICATION)
            {
                services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();

                services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = false,
                        RequireExpirationTime = false,
                        RequireSignedTokens = false,
                    };
                });
            }
            else
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(this.Configuration);
            }

            services.AddOptions();
            services
                .AddOptions<StorageOptions>()
                .Bind(this.Configuration.GetSection("Storage"))
                .ValidateDataAnnotations();

            services.AddScoped<TableClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<StorageOptions>>().Value;
                var credential = sp.GetRequiredService<TokenCredential>();
                return new TableClient(options.TableEndpoint, options.CoinsTableName, credential);
            });

            services.AddScoped<QueueClient>(sp =>
           {
               var options = sp.GetRequiredService<IOptions<StorageOptions>>().Value;
               var credential = sp.GetRequiredService<TokenCredential>();
               return new QueueClient(options.QueueEndpoint, credential);
           });

            services.AddScoped<ICoinsTableRepository, CoinsTableRepository>();
            services.AddScoped<IWishQueueService, WishQueueService>();
            services.AddScoped<IWishCountService, WishCountService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WishingWell.Api", Version = "v1" });
            });

            var appInsightsKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATION_KEY");
            if (appInsightsKey != null)
            {
                services.AddApplicationInsightsTelemetry(appInsightsKey);
            }
        }

        // This method gets called by the runtime when ASPNETCORE_ENVIRONMENT is set to Development
        // However, this convention will not call ConfigureServices(...) so we manually invoke it here, assuming it is our base configuration
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);
            services.AddScoped<TokenCredential>(sp => new DefaultAzureCredential());
        }

        // This method gets called by the runtime when ASPNETCORE_ENVIRONMENT is set to Production
        public void ConfigureProductionServices(IServiceCollection services)
        {
            this.ConfigureServices(services);
            services.AddScoped<TokenCredential>(sp => new ManagedIdentityCredential());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WishingWell.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
