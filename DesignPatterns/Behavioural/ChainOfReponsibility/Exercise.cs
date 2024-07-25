namespace DesignPatterns.Behavioural.ChainOfReponsibility.Exercise;

public enum Statistic
{
    Attack,
    Defense
}

public class StatQuery
{
    public Statistic Statistic;
    public int Result;
}

public abstract class Creature
{
    protected Game _game;

    protected int baseAttack;
    protected int baseDefence;

    protected Creature(Game game, int baseAttack, int baseDefence)
    {
        this._game = game;
        this.baseAttack = baseAttack;
        this.baseDefence = baseDefence;
    }

    public abstract void Query(object source, StatQuery sq);

    public virtual int Attack { get; set; }
    public virtual int Defense { get; set; }
}

public class Goblin : Creature
{
    public Goblin(Game game) : base(game, 1, 1)
    {
    }

    public Goblin(Game game, int baseAttack, int baseDefence) : base(game, baseAttack, baseDefence)
    { }

    public override void Query(object source, StatQuery sq)
    {
        if (ReferenceEquals(source, this))
        {
            switch (sq.Statistic)
            {
                case Statistic.Attack:
                    sq.Result += baseAttack;
                    break;

                case Statistic.Defense:
                    sq.Result += baseAttack;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            if (sq.Statistic == Statistic.Defense)
            {
                sq.Result++;
            }
        }
    }

    public override int Defense
    {
        get
        {
            var q = new StatQuery { Statistic = Statistic.Defense };
            foreach (var c in _game.Creatures)
                c.Query(this, q);
            return q.Result;
        }
    }

    public override int Attack
    {
        get
        {
            var q = new StatQuery { Statistic = Statistic.Attack };
            foreach (var c in _game.Creatures)
                c.Query(this, q);
            return q.Result;
        }
    }
}

public class GoblinKing : Goblin
{
    public GoblinKing(Game game) : base(game, 3, 3)
    {
    }

    public override void Query(object source, StatQuery sq)
    {
        if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack)
        {
            sq.Result++; // every goblin gets +1 attack
        }
        else base.Query(source, sq);
    }
}

public class Game
{
    private readonly IList<Creature> _creatures = new List<Creature>();

    public IList<Creature> Creatures => _creatures;
}

