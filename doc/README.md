# ASP.NET with Autofac Tutorial

## Step 1 - Install Packages

### Autofac

Autofac is an IoC container for Microsoft .NET.

```
Install-Package Autofac
```

### Autofac.WebApi2

ASP.NET Web API integration for [Autofac](https://autofac.org).

```
Install-Package Autofac.WebApi2
```

## Step 2 - Configure Autofac

Create the `AutofacConfig` class, preferable in the `App_start` directory. This class should do the followings:

1. Create an Autofac container
2. Configure the ASP.NET dependency resolver to use the Autofac container

```C#
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

public class AutofacConfig
{
    public static void RegisterDependencies()
    {
        // 1. Create Autofac container
        
        ContainerBuilder builder = new ContainerBuilder();
        RegisterDependencies(builder);
        IContainer container = builder.Build();

        // 2. Configure the ASP.NET dependency resolver to use the Autofac container
        
        AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
        GlobalConfiguration.Configuration.DependencyResolver = resolver;
    }

    private static void RegisterDependencies(ContainerBuilder builder)
    {
        builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        // Register additional services
        // ...
        builder.RegisterType<MyService>()
                .As<IMyService>()
                .InstancePerRequest();
    }
}
```

## Step 3 - Call the Autofac Config from `Global.asax.c`

In `Application_Start()`:

```C#
protected void Application_Start()
{
	...
    AutofacConfig.RegisterDependencies();
}
```

## Step 4 - Create a service class and interface

```C#
public interface IMyService
{
    string GetMessage();
}

public class MyService : IMyService
{
    public string GetMessage()
    {
        return "Hello Autofac!";
    }
}
```

## Step 5 - Inject the service in the controller

```c#
public class DummyController : ApiController
{
    private readonly IMyService myService;

    public DummyController(IMyService myService)
    {
        this.myService = myService ?? throw new System.ArgumentNullException(nameof(myService));
    }
    
    public string Get()
    {
        return myService.GetMessage();
    }
}
```

