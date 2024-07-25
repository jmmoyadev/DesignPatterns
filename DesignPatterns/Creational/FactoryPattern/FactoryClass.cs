using System.Collections.Generic;

namespace DesignPatterns.Creational.FactoryPattern.FactoryClass;

public class Point
{
    private double x, y;

    internal Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        var x = rho * Math.Cos(theta);
        var y = rho * Math.Sin(theta);

        return new Point(x, y);
    }
}

internal class Demo
{
    private static void Main(string[] args)
    {
        var p1 = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
    }
}