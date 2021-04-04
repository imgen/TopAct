using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using TopAct.Common;
using static TopAct.Common.SharedConstants;

namespace TopAct.WebApi
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
            services.AddControllers();
            var builder = services.AddIdentityServer(options =>
                {
                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    options.EmitStaticAudienceClaim = true;
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);

            services.AddControllers();
            services.AddAuthentication(AuthenticationSchemeName)
                .AddJwtBearer(AuthenticationSchemeName, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = IdentityServerUrl;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TopAct.WebApi", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Type = SecuritySchemeType.OAuth2,
                        Scheme = "Bearer",
                        OpenIdConnectUrl = new Uri($".well-known/openid-configuration", UriKind.Relative),
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                                TokenUrl = new Uri("/connect/token", UriKind.Relative),
                                Scopes = new Dictionary<string, string>
                                {
                                    [SharedConstants.ApiScope] = "Access api"
                                }
                            },
                        },
                        In = ParameterLocation.Header,
                        Name = "Authorization"
                    });

                    var oauthScheme = new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    };

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        { oauthScheme, new List<string>() }
                    });
                });

            services.AddTopActServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TopAct.WebApi v1");
                    c.EnableDeepLinking();

                    // Additional OAuth settings (See https://github.com/swagger-api/swagger-ui/blob/v3.10.0/docs/usage/oauth2.md)
                    c.OAuthClientId(ClientId);
                    c.OAuthClientSecret(ApiSecret);
                    c.OAuthAppName("topact");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
