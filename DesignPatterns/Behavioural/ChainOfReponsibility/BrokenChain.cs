namespace DesignPatterns.Behavioural.ChainOfReponsibility.BrokenChain;

public class Game
{
    public event EventHandler<Query> Queries;

    public void PerformQuery(object sender, Query q)
    {
        Queries?.Invoke(sender, q);
    }
}

public class Query
{
    public string CreatureName { get; set; }

    public enum Argument
    {
        Attack, Defence
    }

    public Argument WhatToQuery { get; set; }

    public int Value { get; set; }

    public Query(string creatureName, Argument whatToQuery, int value)
    {
        CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
        WhatToQuery = whatToQuery;
        Value = value;
    }
}

public class Creature
{
    private readonly Game _game;
    public string Name { get; set; }
    private int _attack, _defence;

    public Creature(Game game, string name, int attack, int defence)
    {
        this._game = game ?? throw new ArgumentNullException(nameof(game));

        Name = name ?? throw new ArgumentNullException(nameof(name));

        this._attack = attack;
        this._defence = defence;
    }

    public int Attack
    {
        get
        {
            var q = new Query(this.Name, Query.Argument.Attack, _attack);
            _game.PerformQuery(this, q);
            return q.Value;
        }
    }

    public int Defence
    {
        get
        {
            var q = new Query(this.Name, Query.Argument.Defence, _defence);
            _game.PerformQuery(this, q);
            return q.Value;
        }
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name},  {nameof(Attack)}: {Attack}, {nameof(Defence)}: {Defence}";
    }
}

public abstract class CreatureModifier : IDisposable
{
    protected Game _game;
    protected Creature _creature;

    protected CreatureModifier(Game game, Creature creature)
    {
        _game = game ?? throw new ArgumentNullException(nameof(game));
        _creature = creature ?? throw new ArgumentNullException(nameof(creature));
        _game.Queries += Handle;
    }

    protected abstract void Handle(object sender, Query q);

    public void Dispose()
    {
        _game.Queries -= Handle;
    }
}

public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
    {
    }

    protected override void Handle(object sender, Query q)
    {
        if (q.CreatureName == _creature.Name
            && q.WhatToQuery == Query.Argument.Attack)
        {
            q.Value *= 2;
        }
    }
}

public class IncreaseDefenceModifier : CreatureModifier
{
    public IncreaseDefenceModifier(Game game, Creature creature) : base(game, creature)
    {
    }

    protected override void Handle(object sender, Query q)
    {
        if (q.CreatureName == _creature.Name
            && q.WhatToQuery == Query.Argument.Defence)
        {
            q.Value += 2;
        }
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var game = new Game();
        var goblin = new Creature(game, "Strong Goblin", 3, 3);
        Console.WriteLine(goblin);

        using (new DoubleAttackModifier(game, goblin))
        {
            Console.WriteLine(goblin);

            using (new IncreaseDefenceModifier(game, goblin))
            {
                Console.WriteLine(goblin);
            }
        }

        Console.WriteLine(goblin);
    }
}
