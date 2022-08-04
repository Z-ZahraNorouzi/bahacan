using Microsoft.AspNetCore.Authentication;
using Service.Security;
using Service.BaseClasses;
using System.Text.Json.Serialization;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".AspNetCore.Session";
            });

            services.AddAuthentication("Bearer")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Bearer", null);
            //services.AddJwtAuthentication();

            services.AddScoped<ISessionManager, SessionManager>();
            //services.AddScoped<IJwtService, JwtService>();

            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
                setupAction.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            }); //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost", "http://localhost:7150")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    }
                );
            });
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            #region Swagger
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //    {
            //        Title = "Place Info Service API",
            //        Version = "v1",
            //        Description = "Sample service for Learner",
            //    });


            //    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.Http,
            //        Scheme = "basic",
            //        In = ParameterLocation.Header,
            //        Description = "Basic Authorization header using the Bearer scheme."
            //    });

            //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //              new OpenApiSecurityScheme
            //                {
            //                    Reference = new OpenApiReference
            //                    {
            //                        Type = ReferenceType.SecurityScheme,
            //                        Id = "basic"
            //                    }
            //                },
            //                new string[] {}
            //        }
            //    });
            //});
            #endregion
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseDeveloperExceptionPage();// app.UseExceptionHandler("/error"); //

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            //app.UseSwagger();
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}