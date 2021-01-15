# <img align="center" src="https://raw.githubusercontent.com/dalrankov/TransactChoco/master/icon.png"/> TransactChoco

TransactionScope middleware for HotChocolate GraphQL

<a href="https://www.nuget.org/packages/TransactChoco"><img alt="NuGet Version" src="https://img.shields.io/nuget/v/TransactChoco"></a>
<a href="https://www.nuget.org/packages/TransactChoco"><img alt="NuGet Downloads" src="https://img.shields.io/nuget/dt/TransactChoco"></a>

## How it works?

Registered TransactionScope middleware will be executed on top of GraphQL field and wrap nested middlewares and resolvers in new TransactionScope, completing it upon successfull execution.

## Usage

Code first:
````csharp
descriptor.Field("demo")
    .UseTransactionScope();
````

Pure code first:
````csharp
[UseTransactionScope]
public string Demo() => "Some DB work here!";
````

Default TransactionScope factory has following specs:
- TransactionScopeOption: Required
- IsolationLevel: ReadCommited
- Timeout: 1 minute
- TransactionScopeAsyncFlowOption: Enabled

## Custom TransactionScope factory

You can implement `ITransactionScopeFactory` to build your own TransactionScope objects.

````csharp
public class CustomTransactionScopeFactory
    : ITransactionScopeFactory
{
    public TransactionScope CreateScope(
        IMiddlewareContext context)
    {
        return new TransactionScope();
    }
}
````

Code first:
````csharp
descriptor.Field("demo")
    .UseTransactionScope<CustomTransactionScopeFactory>();
````

Pure code first:
````csharp
[UseTransactionScope(typeof(CustomTransactionScopeFactory))]
public string Demo() => "Some DB work here!";
````

------------------------
Icon made by [Ivana Vujačić](https://www.pinterest.com/vujacicnivana/).