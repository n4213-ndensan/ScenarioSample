# Design-time Service Registration for Radzen Blazor Studio

Problem:

* A component distributed via PackageReference requires a DI service.
* The service is registered through an `IHostApplicationBuilder` extension method.
* When the component is rendered in the Radzen designer, the required service is not available.
* Therefore the component cannot be rendered in the designer.

Current workaround:

* Disable injection under `RADZEN`.

Issue with the workaround:

* The package is normally built in Release mode.
* Consumers of the package still require the service registration.
* Maintaining a separate Radzen-specific package would be undesirable.

Question:

* Is there a recommended way to provide design-time service registration for components used in Radzen Blazor Studio?

## Dependency Sample

```text
Application
    │
    └── PackageReference
            │
            ▼
      UIComponents.nupkg
            │
            ├── MyComponent
            └── MyService (DI)
```

## Service Registration Sample

The component library registers services using an `IHostApplicationBuilder` extension method:

```csharp
public static class MyServiceExtensions
{
    public static IHostApplicationBuilder AddMyServices(this IHostApplicationBuilder builder)
    {
        // Service registration may depend on configuration.
        // Example:
        // var configValue =
        //     builder.Configuration.GetValue<string>("MyServiceConfig");

        builder.Services.AddScoped<MyService>();

        return builder;
    }
}
```

The consuming application registers the services as follows:

```csharp
builder.AddMyServices();
```

Please refer to the sample source code in this repository for the complete implementation details.
