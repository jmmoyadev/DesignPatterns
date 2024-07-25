namespace DesignPatterns.Creational.FactoryPattern.AbstractFactory;

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffe is sensational!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

public class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
        return new Tea();
    }
}

public class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Gind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    public enum AvailableDrink
    {
        Coffee, Tea
    }

    private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
      new Dictionary<AvailableDrink, IHotDrinkFactory>();

    public HotDrinkMachine()
    {
        var assembly = typeof(HotDrinkMachine).Assembly.GetName().Name;

        foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        {
            var typeName = $"{GetType().Namespace}.{Enum.GetName(typeof(AvailableDrink), drink)}Factory";
            var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(typeName));

            factories.Add(drink, factory);
        }
    }

    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
        return factories[drink].Prepare(amount);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink1 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
        drink1.Consume();

        var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 20);
        drink2.Consume();
    }
}