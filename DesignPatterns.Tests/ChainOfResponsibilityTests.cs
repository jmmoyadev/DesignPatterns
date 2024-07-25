using DesignPatterns.Behavioural.ChainOfReponsibility.Exercise;

namespace DesignPatterns.Tests;

[TestFixture]
internal class ChainOfResponsibilityTests
{
    [Test]
    public void When_OneGoblinInGame()
    {
        var game = new Game();
        var goblin = new Goblin(game);

        game.Creatures.Add(goblin);

        Assert.That(goblin.Attack, Is.EqualTo(1));
        Assert.That(goblin.Defense, Is.EqualTo(1));
    }

    [Test]
    public void When_ThreeGoblinsInGame()
    {
        var game = new Game();

        var goblin1 = new Goblin(game);
        var goblin2 = new Goblin(game);
        var goblin3 = new Goblin(game);

        game.Creatures.Add(goblin1);
        game.Creatures.Add(goblin2);
        game.Creatures.Add(goblin3);

        Assert.That(goblin1.Attack, Is.EqualTo(1));
        Assert.That(goblin1.Defense, Is.EqualTo(3));

        Assert.That(goblin2.Attack, Is.EqualTo(1));
        Assert.That(goblin2.Defense, Is.EqualTo(3));

        Assert.That(goblin3.Attack, Is.EqualTo(1));
        Assert.That(goblin3.Defense, Is.EqualTo(3));
    }

    [Test]
    public void When_GoblinKingIsInGame()
    {
        var game = new Game();

        var goblinKing = new GoblinKing(game);
        var goblin = new Goblin(game);

        game.Creatures.Add(goblinKing);
        game.Creatures.Add(goblin);

        Assert.That(goblinKing.Attack, Is.EqualTo(3));
        Assert.That(goblinKing.Defense, Is.EqualTo(4));

        Assert.That(goblin.Attack, Is.EqualTo(2));
        Assert.That(goblin.Defense, Is.EqualTo(2));
    }

    [Test]
    public void ManyGoblinsTest()
    {
        var game = new Game();
        var goblin = new Goblin(game);
        game.Creatures.Add(goblin);

        Assert.That(goblin.Attack, Is.EqualTo(1));
        Assert.That(goblin.Defense, Is.EqualTo(1));

        var goblin2 = new Goblin(game);
        game.Creatures.Add(goblin2);

        Assert.That(goblin.Attack, Is.EqualTo(1));
        Assert.That(goblin.Defense, Is.EqualTo(2));

        var goblin3 = new GoblinKing(game);
        game.Creatures.Add(goblin3);

        Assert.That(goblin.Attack, Is.EqualTo(2));
        Assert.That(goblin.Defense, Is.EqualTo(3));
    }
}