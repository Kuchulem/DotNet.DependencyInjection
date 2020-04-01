# Kuchulem.DotNet.DependencyInjection

Kuchulem's .Net dependency injection service is a .net standard service to resolve dependencies.

>This is some experimental stuff designed because I was not happy with Xamarin.Forms.DependencyService.

Quite easy to use it allows you to have _transcient_ dependencies (instantiated every time the service is called) or _singletons_ (instanciated only once).

## how to use

A fully featured documentation is available in the github [wiki](https://github.com/Kuchulem/DotNet.DependencyInjection/wiki).

Let's assume we develop a console application to display a numerical sequence :

```csharp
namespace NumericalSequence
{
    class Program
    {
        // this is where the magic occures
    }
}
```

Let's design a service interface for return a numerical sequence next value  ...

```csharp
namespace DependencyInjection.Demo.Services
{
    interface INumericalSequenceService
    {
        // Every numerical sequence has some initialization values
        List<int> InitialValues { get; }

        // Will return the next value for the sequence
        int Next();
    } 
}

```

Now an implementation of the service, let's say the famous fibonacci sequence :

```csharp
namespace DependencyInjection.Demo.Services
{
    class FibonacciSequenceService : INumericalSequenceService
    {
        // Every numerical sequence has some initialization values
        public List<int> InitialValues { get; }

        public FibonacciSequenceService()
        {
            InitialValues = new  new List<int> { 0, 1 } 
        }        

        public int Next()
        {
            // get the 2 last values and add them 
            var value = InitialValues[InitialValues.Count - 1] + InitialValues[InitialValues.Count - 2];

            // add the result to initial values to remember it
            InitialValues.Add(value);

            return value;
        }
    } 
}
```

And now register that service in our program.


```csharp
using Kuchulem.DotNet.DepedencyInjection;
using NumericalSequence.Services;

namespace NumericalSequence
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DependencyInjectionService.Register<INumericalSequenceService, FibonacciSequenceService>();
        }
    }
}
```

To retrieve the isntance, quite easy :

```csharp

// service var will contain the FibonacciSequenceService but is exposed as the interface
INumericalSequenceService service = DependencyInjectionService.Resolve<INumericalSequenceService>();

```

# What else ?

## Xamarin forms compatibility

The `DependencyInjectService` allows you to register an assembly rather than an implementation. This is helpfull when working on xamarin forms projects and loading dependencies that are dependent to the OS.

## Disclaimer

The library is actually in Beta state, meaning you use it at your own risks. Be aware of that. I tested it lightly and started to use it in my own projects.

Thought I may not add features to the lib I will fix it as fast as I can if needed.