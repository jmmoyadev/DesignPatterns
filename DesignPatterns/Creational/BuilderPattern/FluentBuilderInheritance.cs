namespace DesignPatterns.Creational.BuilderPattern.FluentBuilderInheritance;

// <summary>
//
// </summary>

public class Person
{
    public string Name;

    public string Position;

    public DateTime DateOfBirth;

    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder()
        { }
    }

    public static Builder New => new Builder();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new Person();

    public Person Build()
    {
        return person;
    }
}

public class PersonInfoBuilder<TSelf> : PersonBuilder
  where TSelf : PersonInfoBuilder<TSelf>
{
    public TSelf Called(string name)
    {
        person.Name = name;
        return (TSelf)this;
    }
}

public class PersonJobBuilder<TSelf>
  : PersonInfoBuilder<PersonJobBuilder<TSelf>>
  where TSelf : PersonJobBuilder<TSelf>
{
    public TSelf WorksAsA(string position)
    {
        person.Position = position;
        return (TSelf)this;
    }
}

// here's another inheritance level
// note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

public class PersonBirthDateBuilder<TSelf>
  : PersonJobBuilder<PersonBirthDateBuilder<TSelf>>
  where TSelf : PersonBirthDateBuilder<TSelf>
{
    public TSelf Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (TSelf)this;
    }
}

public class Program
{
    private class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
    {
    }

    public static void Main(string[] args)
    {
        var me = Person.New
          .Called("John")
          .WorksAsA("Quant")
          .Born(DateTime.UtcNow)
          .Build();
        Console.WriteLine(me);
    }
}