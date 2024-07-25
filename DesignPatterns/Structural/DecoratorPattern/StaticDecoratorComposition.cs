namespace DesignPatterns.Structural.DecoratorPattern.StaticDecoratorComposition;

public abstract class Shape
{
    public virtual string AsString() => string.Empty;
}

public class Circle : Shape
{
    private float radius;

    public Circle() : this(0.0f)
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

    public override string AsString()
    {
        return $"A circle of radius {radius}";
    }
}

public class Square : Shape
{
    private float side;

    public Square() : this(0.0f)
    {
    }

    public Square(float side)
    {
        this.side = side;
    }

    public override string AsString()
    {
        return $"A square with side {side}";
    }
}

public class ColoredShape : Shape
{
    private Shape shape;
    private string color;

    public ColoredShape(Shape shape, string color)
    {
        this.shape = shape;
        this.color = color;
    }

    public override string AsString()
    {
        return $"{shape.AsString()} has de color {color}";
    }
}

public class ColoredShape<T> : Shape where T : Shape, new()
{
    private string color;
    private T shape = new T();

    public ColoredShape() : this("black")
    {
    }

    public ColoredShape(string color)
    {
        this.color = color;
    }

    public override string AsString()
    {
        return $"{shape.AsString()} has de color {color}";
    }
}

public class TransparentShape : Shape
{
    private Shape shape;
    private float transparency;

    public TransparentShape(Shape shape, float transparency)
    {
        this.shape = shape;
        this.transparency = transparency;
    }

    public override string AsString()
    {
        return $"{shape.AsString()} has {transparency * 100.0f}% transparency";
    }
}

public class TransparentShape<T> : Shape where T : Shape, new()
{
    private T shape = new T();
    private float transparency;

    public TransparentShape() : this(0)
    {
    }

    public TransparentShape(float transparency)
    {
        this.transparency = transparency;
    }

    public override string AsString()
    {
        return $"{shape.AsString()} has {transparency * 100.0f}% transparency";
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var redSquare = new ColoredShape<Square>("red");
        Console.WriteLine(redSquare.AsString());

        var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
        Console.WriteLine(circle.AsString());
    }
}