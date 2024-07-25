using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.DecoratorPattern.MultipleInheritanceWithInterfaces;

public interface IBird
{
    void Fly();
}

public class Bird : IBird
{
    public void Fly()
    {
    }
}

public interface ILizard
{
    void Crawl();
}

public class Lizard : ILizard
{
    public void Crawl()
    {
    }
}

public class Dragon : IBird, ILizard
{
    private readonly Bird bird = new Bird();

    private readonly Lizard lizard = new Lizard();

    public Dragon()
    {
    }

    public void Crawl()
    {
        lizard.Crawl();
    }

    public void Fly()
    {
        bird.Fly();
    }
}