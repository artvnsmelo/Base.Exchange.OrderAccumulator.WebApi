using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

namespace Base.Exchange.OrderAccumulator.WebApi.API.Configuration;


[ExcludeFromCodeCoverage]
public static class SwaggerDocsStartup
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var swaggerConfig = configuration?.GetSection(SwaggerSettings.SectionName)?.Get<SwaggerSettings>() ?? new();

        services.AddSwaggerGen(options =>
        {
            options.ConfigureSwaggerDocInfo(swaggerConfig);
            options.ConfigureSwaggerDefaultSettings(swaggerConfig);
            options.ConfigureSwaggerJwtSecurity(swaggerConfig);
            options.ConfigureSwaggerDocServerUrl(swaggerConfig);
        });

        return services;
    }

    private static void ConfigureSwaggerDocInfo(this SwaggerGenOptions swagger, SwaggerSettings config)
    {
        if (swagger is null) throw new ArgumentNullException(nameof(swagger));

        swagger.SwaggerDoc
        (
            config.Version,
            new OpenApiInfo
            {
                Title = config.Title,
                Version = config.Version,
                Description = config.Description,
                Contact = new OpenApiContact
                {
                    Name = config.ContactName,
                    Email = config.ContactEmail,
                    Url = config.ContactUri
                },
                License = new OpenApiLicense
                {
                    Name = config.LicenseName,
                    Url = config.LicenseUri
                },
                TermsOfService = config.TermsOfServiceUri
            }
        );
    }

    private static void ConfigureSwaggerDefaultSettings
    (
        this SwaggerGenOptions swagger,
        SwaggerSettings config
    )
    {
        if (swagger is null) throw new ArgumentNullException(nameof(swagger));

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        swagger.IncludeXmlComments(xmlPath);

        swagger.TagActionsBy(t =>
        {
            if (t.GroupName != null)
            {
                return new[] { t.GroupName };
            }

            return t.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                ? (IList<string>)(new[] { controllerActionDescriptor.ControllerName })
                : throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });

        swagger.DocInclusionPredicate((name, api) => true);
        swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        swagger.CustomSchemaIds(type => type.ToString().Replace("`1", "_").Replace("[", "").Replace("]", ""));
    }

    private static void ConfigureSwaggerJwtSecurity
    (
        this SwaggerGenOptions swagger,
        SwaggerSettings config
    )
    {
        if (swagger is null) throw new ArgumentNullException(nameof(swagger));

        if (!config.Security.Enabled) return;

        swagger.AddSecurityDefinition(config.Security.Scheme, new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = config.Security.Scheme,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = @"JWT Authorization header using the Bearer scheme.
                   \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
                    \r\n\r\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMj...'",
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = config.Security.Scheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
    }

    private static void ConfigureSwaggerDocServerUrl
    (
        this SwaggerGenOptions swagger,
        SwaggerSettings config
    )
    {
        if (swagger == null) throw new ArgumentNullException(nameof(swagger));

        var url = config?.Server?.Url;

        if (string.IsNullOrWhiteSpace(url)) return;

        swagger.AddServer(new OpenApiServer { Url = url });
    }

    public static void UseSwaggerDocs(this WebApplication app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        if (!app.IsSwaggerEnabled()) return;

        app.UseSwagger();
        app.UseSwaggerUI(options => options.DefaultModelsExpandDepth(-1));
    }

    private static bool IsSwaggerEnabled(this WebApplication app)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var swaggerEnabled = !
        (
            string.IsNullOrWhiteSpace(env) &&
            app.Environment.IsProduction() &&
            app.Environment.IsStaging() &&
            app.Environment.IsEnvironment("test")
        );

        return swaggerEnabled;
    }
}
