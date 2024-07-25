namespace DesignPatterns.Creational.FactoryPattern.FactoryMethod;

public class Point
{
    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    // factory method
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        var a = rho * Math.Cos(theta);
        var b = rho * Math.Sin(theta);

        return new Point(a, b);
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    //// make it lazy
    //public static class Factory
    //{
    //    public static Point NewCartesianPoint(double x, double y)
    //    {
    //        return new Point(x, y);
    //    }

    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        var x = rho * Math.Cos(theta);
    //        var y = rho * Math.Sin(theta);

    //        return new Point(x, y);
    //    }
    //}
}

internal class Demo
{
    private static void Main(string[] args)
    {
        var p1 = Point.NewPolarPoint(1.0, Math.PI / 2);
    }
}