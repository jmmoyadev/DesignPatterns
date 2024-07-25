namespace DesignPatterns.Creational.FactoryPattern.AsynchronouseFactoryMethod;

public class Foo
{
    private Foo()
    {
        // ...
    }

    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    public static Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();
    }
}

public class Demo
{
    public static async Task MainAsync()
    {
        var foo1 = await Foo.CreateAsync();
    }
}