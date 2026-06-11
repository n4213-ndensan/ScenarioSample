# Design-time Issues for Radzen Blazor Studio

This repository contains a small sample that shows a design-time issue in Radzen Blazor Studio.

In a normal application, a Razor component can use JavaScript functions that are registered by the host app. In the Radzen designer, the component is rendered in a different environment, so those scripts may not be available yet.

The section below marked as resolved describes an issue that has already been fixed. A separate new problem is being added in this sample.

## What this sample demonstrates

This sample reports a design-time issue in Radzen Blazor Studio.

`MyComponent` in `UIComponents.nupkg` uses the global JavaScript function `globalFunction`. In the normal app, `MyScripts.js` provides that function.

In the Radzen designer, `MyComponent` is rendered without that function being available.

```text
Application
    │
    └── PackageReference
            │
            ▼
      UIComponents.nupkg
            │
            ├── MyComponent (Razor component)
            └── wwwroot
                └── MyScripts.js (defines a global JavaScript function: `function globalFunction() { ... }`)
```

In short, the sample shows that the component depends on `globalFunction` in the app, but the designer does not provide it.

## Background of the resolved issue

When a component comes from a `PackageReference`, it may depend on a DI service registered by the consuming app. This sample registers that service through an `IHostApplicationBuilder` extension method.

In the Radzen designer, the same service registration is not available during design-time rendering, so the component cannot initialize correctly.

<details>
<summary>Resolved issue</summary>

The component library depends on a DI service.

The service is registered with an `IHostApplicationBuilder` extension method.

Radzen's design-time environment does not see that registration.

As a result, the component fails to render in the designer.

### Current workaround

Disable injection under `RADZEN`.

### Why that workaround is not ideal

The package is usually built in Release mode.

Consumers of the package still need the service registration.

Keeping a separate Radzen-only package would add extra maintenance.

### Question

Is there a recommended way to provide design-time service registration for components used in Radzen Blazor Studio?


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

The component library registers services with an `IHostApplicationBuilder` extension method:

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

The consuming application registers the services like this:

```csharp
builder.AddMyServices();
```
</details>

Please refer to the sample source code in this repository for the full implementation details.
