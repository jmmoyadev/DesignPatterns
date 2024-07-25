using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.FactoryPattern.AbstractFactoryOCP;

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
    //    public enum AvailableDrink
    //    {
    //        Coffee, Tea
    //    }

    //    private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
    //      new Dictionary<AvailableDrink, IHotDrinkFactory>();

    //public HotDrinkMachine()
    //{
    //    var assembly = typeof(HotDrinkMachine).Assembly.GetName().Name;

    //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        var typeName = $"{GetType().Namespace}.{Enum.GetName(typeof(AvailableDrink), drink)}Factory";
    //        var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(typeName));

    //        factories.Add(drink, factory);
    //    }
    //}

    private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            {
                factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty),
                    (IHotDrinkFactory)Activator.CreateInstance(t)));
            }
        }
    }

    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Available drinks:");
        for (int i = 0; i < factories.Count; i++)
        {
            var tuple = factories[i];
            Console.WriteLine($"{i}: {tuple.Item1}");
        }

        while (true)
        {
            string s;
            if ((s = Console.ReadLine()) != null
                && int.TryParse(s, out int i)
                && i >= 0
                && i < factories.Count)
            {
                Console.WriteLine("Specify amount: ");
                s = Console.ReadLine();

                if (s != null
                    && int.TryParse(s, out int amount)
                    && amount > 0)
                {
                    return factories[i].Item2.Prepare(amount);
                }
            }
            Console.WriteLine("Incorrect input, try again!");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink1 = machine.MakeDrink();
        drink1.Consume();
    }
}