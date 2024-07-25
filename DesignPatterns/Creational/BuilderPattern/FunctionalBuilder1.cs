namespace DesignPatterns.Creational.BuilderPattern.FunctionalBuilder1;

/// <summary>
/// Builder that uses a functional approach.
/// </summary>
public class FunctionalBuilder
{
}

public class Person
{
    public string Name, Position;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public sealed class PersonBuilder
{
    public readonly List<Action<Person>> Actions
      = new List<Action<Person>>();

    public PersonBuilder Called(string name)
    {
        Actions.Add(p => { p.Name = name; });
        return this;
    }

    public Person Build()
    {
        var p = new Person();
        Actions.ForEach(a => a(p));
        return p;
    }
}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
    {
        builder.Actions.Add(p =>
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
        var person = pb.Called("John").WorksAsA("Programmer").Build();
        Console.WriteLine(person);
    }
}