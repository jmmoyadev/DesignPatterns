using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.BuilderPattern.FunctionalBuilder2;

public class Person
{
    public string Name, Position;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();

    public TSelf Do(Action<TSubject> action) => AddAction(action);

    private TSelf AddAction(Action<TSubject> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });
        return (TSelf)this;
    }

    public TSubject Build() => actions.Aggregate(new TSubject(), (p, f) => f(p));
}

public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
    {
        builder.Do(p =>
        {
            p.Position = position;
        });
        return builder;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var pb = new PersonBuilder();

        var person = pb.Called("Sarah")
                       .WorksAsA("Developer")
                       .Build();

        Console.WriteLine(person);
    }
}