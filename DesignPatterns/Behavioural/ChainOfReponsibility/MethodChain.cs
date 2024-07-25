namespace DesignPatterns.Behavioural.ChainOfReponsibility.MethodChain;

public class Creature
{
    public string Name { get; set; }
    public virtual int Attack { get; set; }
    public virtual int Defence { get; set; }

    public Creature(string name, int attack, int defence)
    {
        Name = name;
        Attack = attack;
        Defence = defence;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defence)}: {Defence}";
    }
}

public class CreatureModifier
{
    protected Creature _creature;
    protected CreatureModifier _next; // linked list

    public CreatureModifier(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);
        _creature = creature;
    }

    public void Add(CreatureModifier creatureModifier)
    {
        if (_next != null) _next.Add(creatureModifier);
        else _next = creatureModifier;
    }

    public virtual void Handle() => _next?.Handle();
}

public class DoubleAttachModifier : CreatureModifier
{
    public DoubleAttachModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Doubling {_creature.Name}'s attack");
        this._creature.Attack *= 2;

        base.Handle();
    }
}

public class IncreaseDefenceModifier : CreatureModifier
{
    public IncreaseDefenceModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Increasing {_creature.Name}'s defence");
        this._creature.Defence += 3;
        base.Handle();
    }
}

public class NoBonusesModifier : CreatureModifier
{
    public NoBonusesModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        // do nothing
        Console.WriteLine($"{_creature.Name} cannot be buffed");
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var goblin = new Creature("Goblin", 2, 2);
        Console.WriteLine(goblin);

        var root = new CreatureModifier(goblin);

        root.Add(new NoBonusesModifier(goblin));

        Console.WriteLine("Lets double the goblin's attack");
        root.Add(new DoubleAttachModifier(goblin));

        Console.WriteLine("Lets increase the goblin's defence");
        root.Add(new IncreaseDefenceModifier(goblin));

        root.Handle();

        Console.WriteLine(goblin);
    }
}
