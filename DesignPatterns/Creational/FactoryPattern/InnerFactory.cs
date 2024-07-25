namespace DesignPatterns.Creational.FactoryPattern.InnerFactory;

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

    // factory property
    public static Point Origin => new Point(0, 0);

    // singleton field
    public static Point Origin2 = new Point(0, 0); // better

    public static class Factory
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
}

internal class Demo
{
    private static void Main(string[] args)
    {
        var p1 = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
    }
}