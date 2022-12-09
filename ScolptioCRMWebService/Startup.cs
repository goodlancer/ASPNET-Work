
using CommandHandler;

using Commands;

using Domains.ConfigSetting;
using Domains.ConstModels;
using Domains.DBModels;

using FluentValidation;

using ScolptioCRMWebService.Helpers;
using ScolptioCRMWebService.Pipeline;
using ScolptioCRMWebService.Validations;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MongoDbGenericRepository;

using ScolptioCRMCoreService.IManagers;
using ScolptioCRMCoreService.Managers;
using ScolptioCRMCoreService.Services;
using ScolptioCRMWebApi.Cron;
using Services.IManagers;
using Services.IServices;
using Services.Managers;
using Services.Repository;
using Services.Services;

using System.Text;
using System.Text.Json.Serialization;

namespace ScolptioCRMWebService
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            SchedulerTask.StartAsync().GetAwaiter().GetResult();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(Configuration.GetSection("CorsUrl").Value)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod(); ;
                                  });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScolptioCRMCoreService.Service", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

            });
            services.Configure<PostCardManiaSetting>(Configuration.GetSection("PostCardManiaCredential"));
            services.Configure<PostCardManiaUrl>(Configuration.GetSection("PostCardManiaUrl"));

            services.Configure<Mongosettings>(Configuration.GetSection("Mongosettings"));
            services.Configure<AwsSettings>(Configuration.GetSection("AwsSettings"));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); 

            services.AddSwaggerGen();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
            services.AddMediatR(typeof(CreateUserCommand).Assembly, typeof(CreateUserCommandHandler).Assembly);

            services.AddTransient<IBaseUserManager, BaseUserManager>();
            services.AddTransient(typeof(IBaseRepository<>), (typeof(BaseRepository<>)));
            services.AddTransient<IMongoScolptioDBContext, MongoScolptioDBContext>();
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<IOrganizationManager, OrganizationManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IMailManager, MailManager>();
            services.AddSingleton<PostCardManiaService>();

            var mongoDbContext = new MongoDbContext(Configuration.GetSection("Mongosettings:Connection").Value, Configuration.GetSection("Mongosettings:DatabaseName").Value);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddMongoDbStores<IMongoDbContext>(mongoDbContext)
              .AddDefaultTokenProviders();

            services.AddMvc();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin",
            //        builder => builder.WithOrigins("http://localhost:4200", "https://ph-saas-ui.propertyhatch.com").AllowAnyHeader().AllowAnyMethod());
            //});

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };

            });
            services.AddAuthorization();
            services.AddApplicationInsightsTelemetry();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "scolptio-crm.Service v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCors();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
