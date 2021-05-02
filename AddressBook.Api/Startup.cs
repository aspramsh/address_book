using AddressBook.Api.Configuration;
using AddressBook.Business;
using AddressBook.Business.Configuration.Models;
using AddressBook.Common.Helpers;
using AddressBook.Common.Mvc;
using AddressBook.DataAccess;
using AddressBook.DataAccess.DataSeeds;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace AddressBook
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
            services.AddOptions();
            services.AddConfigurations(Configuration);
            services.AddAutoMapper(
                cfg => { cfg.AllowNullCollections = true; },
                AppDomain.CurrentDomain.GetAssemblies().Where(x =>
                    x.FullName.Contains(nameof(AddressBook), StringComparison.InvariantCultureIgnoreCase)),
                ServiceLifetime.Scoped);

            services.AddCors(options =>
            {
                options.AddPolicy(nameof(AddressBook),
                    builder =>
                    {
                        builder.WithOrigins()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<Startup>();
                    config.ImplicitlyValidateChildProperties = true;
                });

            #region Api versioning

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            #endregion Api versioning

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressBook.Api", Version = "v1" });
            });
            services.AddDatabase(Configuration);
            services.AddRepositories();
            services.RegisterServices();
            var httpRetryPolicySettings =
                Configuration.GetSection(nameof(HttpRetryPolicySettings)).Get<HttpRetryPolicySettings>();

            services.RegisterHttpClients(httpRetryPolicySettings);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AddressBookContext>();

                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressBook API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCustomExceptionHandler();

            app.UseRouting();

            app.UseCors(nameof(AddressBook));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AsyncUtil.RunSync(() => app.ApplicationServices.GetRequiredService<IDataSeed>()
                .SeedAllInitialDataAsync(applicationLifetime.ApplicationStopping));
        }
    }
}
