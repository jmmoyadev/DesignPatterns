using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.DecoratorPattern.Exercise;

public class Bird
{
    public int Age { get; set; }

    public string Fly()
    {
        return (Age < 10) ? "flying" : "too old";
    }
}

public class Lizard
{
    public int Age { get; set; }

    public string Crawl()
    {
        return (Age > 1) ? "crawling" : "too young";
    }
}

public class Dragon // no need for interfaces
{
    private Bird bird = new Bird();
    private Lizard lizard = new Lizard();

    private int _age;

    public int Age
    {
        set
        {
            _age = value;
            bird.Age = _age;
            lizard.Age = _age;
        }
        get
        {
            return _age;
        }
    }

    public string Fly()
    {
        return bird.Fly();
    }

    public string Crawl()
    {
        return lizard.Crawl();
    }
}