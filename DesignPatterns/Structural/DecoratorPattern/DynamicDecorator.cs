namespace DesignPatterns.Structural.DecoratorPattern.DynamicDecorator;

public interface IShape
{
    string AsString();
}

public class Circle : IShape
{
    private float radius;

    public Circle()
    {
    }

    public Circle(float radius)
    {
        this.radius = radius;
    }

    public void Resize(float factor)
    {
        radius *= factor;
    }

    public string AsString()
    {
        return $"A circle of radius {radius}";
    }
}

public class Square : IShape
{
    private float side;

    public Square()
    {
    }

    public Square(float side)
    {
        this.side = side;
    }

    public string AsString()
    {
        return $"A square with side {side}";
    }
}

public class ColoredShape : IShape
{
    private IShape shape;
    private string color;

    public ColoredShape(IShape shape, string color)
    {
        this.shape = shape;
        this.color = color;
    }

    public string AsString()
    {
        return $"{shape.AsString()} has de color {color}";
    }
}

public class TransparentShape : IShape
{
    private IShape shape;
    private float transparency;

    public TransparentShape(IShape shape, float transparency)
    {
        this.shape = shape;
        this.transparency = transparency;
    }

    public string AsString()
    {
        return $"{shape.AsString()} has {transparency * 100.00}% transparency";
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var square = new Square(1.23f);
        Console.WriteLine(square.AsString());

        var redSquare = new ColoredShape(square, "red");
        Console.WriteLine(redSquare.AsString());

        var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
        Console.WriteLine(redHalfTransparentSquare.AsString());
    }
}