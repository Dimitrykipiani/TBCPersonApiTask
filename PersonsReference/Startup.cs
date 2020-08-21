using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Abstraction;
using Application.Services.Implementation;
using DataContext;
using DataContext.Contexts;
using DataContext.Repositories.Abstraction;
using DataContext.Repositories.Implementation;
using Domain.Models.InputModels.Person;
using Domain.Models.InputModels.Person.Validations;
using Domain.Models.PersonAggregate;
using Domain.Models.PersonAggregate.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonsReference.Utility.Filters;

namespace PersonsReference
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
            services.AddDbContext<PersonReferenceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(PersonReferenceDbContext)));
            });

            services.AddMvc(options => {
                options.Filters.Add(typeof(CheckIfModelStateIsValidFilter));
            })
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<UnitOfWork, UnitOfWork>();

            services.AddTransient<IValidator<Person>, PersonValidator>();
            services.AddTransient<IValidator<UpdatePersonModel>, UpdatePersonModelValidator>();

            services.AddLocalization();

            var georgianCultureInfo = new CultureInfo("ka-GE");
            var englishCultureInfo = new CultureInfo("en-US");

            var supportedCultures = new List<CultureInfo>
            {
                georgianCultureInfo,
                englishCultureInfo
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(georgianCultureInfo);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PersonReferenceDbContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }
    }
}
