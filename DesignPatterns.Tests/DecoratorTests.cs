using DesignPatterns.Structural.DecoratorPattern.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Tests;

[TestFixture]
public class DecoratorTests
{
    [Test]
    public void Test()
    {
        var dragon = new Dragon();

        Assert.That(dragon.Fly(), Is.EqualTo("flying"));
        Assert.That(dragon.Crawl(), Is.EqualTo("too young"));

        dragon.Age = 20;

        Assert.That(dragon.Fly(), Is.EqualTo("too old"));
        Assert.That(dragon.Crawl(), Is.EqualTo("crawling"));
    }
}