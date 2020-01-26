# Kuchulem.DotNet.DependencyInjection

Kuchulem's .Net dependency injection service is a .net standard service to resolve dependencies.

>This is some experimental stuff designed because I was not happy with Xamarin.Forms.DependencyService.

Quite easy to use it allows you to have _transcient_ dependencies (instantiated every time the service is called) or _singletons_ (instanciated only once).

## how to use

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

WIP, more to come :)

Even a propoer doc.