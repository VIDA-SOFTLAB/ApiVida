using System;
using System.Collections.Generic;
using System.Linq;
using ApiVida.JwtDomains;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiVida.Domain.Entities;
using ApiVida.Repository;
using ApiVida.Service;
using ApiVida.Service.Interfaces;

namespace ApiVida
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

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options => {
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = "iris2.s@email.com",
							ValidAudience = "iris2.s@email.com",
							IssuerSigningKey = JwtSecurityKey.Create("a-password-very-big-to-be-good")
						};

						options.Events = new JwtBearerEvents
						{
							OnAuthenticationFailed = context =>
							{
								Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
								return Task.CompletedTask;
							},
							OnTokenValidated = context =>
							{
								Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
								return Task.CompletedTask;
							}
						};
					});

			services.RegisterServices();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
         //   services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseAuthentication();
			app.UseCors("AllowAllHeaders");

            app.UseMvc();

            Repository<AdmEntity>.Initialize("Adm", "VidaBD");
            Repository<PatientEntity>.Initialize("Patient", "VidaBD");	
            Repository<PatientEntity>.Initialize("MedicalInsurancePlan", "VidaBD");	
            Repository<PatientEntity>.Initialize("MedicalInsurance", "VidaBD");	     
            Repository<PatientEntity>.Initialize("MedicalSpeciality", "VidaBD");     
            Repository<PatientEntity>.Initialize("MedicalCenter", "VidaBD");     
            Repository<PatientEntity>.Initialize("Scheduling", "VidaBD");	    
            Repository<PatientEntity>.Initialize("Doctor", "VidaBD");	    
            Repository<PatientEntity>.Initialize("Adress", "VidaBD");     

        }
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<IAdministratorService, AdministratorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IMedicalInsuranceService, MedicalInsuranceService>();
//            services.AddTransient<IMedicalSpecialityService, MedicalSpecialityService>();
            services.AddTransient<IDoctorService, DoctorService>();
  //          services.AddTransient<ISchedulingService, SchedulingService>();
            
            return services;
        }
    }
}
