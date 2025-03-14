using Base.Exchange.OrderAccumulator.WebApi.API.Configuration;

[assembly: RootNamespace("Base.Exchange.OrderAccumulator.WebApi.API")]

var builder = CustomHostBuilder.Create(args);

builder.Services.AddDependencyInjections(builder.Configuration);

var app = builder.Build();


app.UseMiddlewares(builder.Configuration);

app.Run();

#pragma warning disable CA1050
[ExcludeFromCodeCoverage]
public partial class Program { }
#pragma warning restore CA1050
