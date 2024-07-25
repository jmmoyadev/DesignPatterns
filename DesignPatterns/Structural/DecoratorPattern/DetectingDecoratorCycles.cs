using System.Text;

namespace DesignPatterns.Structural.DecoratorPattern.DetectingDecoratorCycles;

public abstract class Shape
{
    public virtual string AsString() => string.Empty;
}

public class Circle : Shape
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

    public override string AsString()
    {
        return $"A circle of radius {radius}";
    }
}

public class Square : Shape
{
    private float side;

    public Square()
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

public abstract class ShapeDecorator : Shape
{
    protected internal readonly List<Type> types = new List<Type>();
    protected internal readonly Shape shape;

    public ShapeDecorator(Shape shape)
    {
        this.shape = shape;
    }
}

public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
    where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
{
    protected readonly TCyclePolicy policy = new TCyclePolicy();

    protected ShapeDecorator(Shape shape) : base(shape)
    {
        if (!policy.TypeAdditionAllowed(typeof(TSelf), types))
        {
            return;
        }

        types.Add(typeof(TSelf));
    }
}

// can determine one policy for all classes
public class ShapeDecoratorWithPolicy<T>
  : ShapeDecorator<T, ThrowOnCyclePolicy>
{
    public ShapeDecoratorWithPolicy(Shape shape) : base(shape)
    {
    }
}

public abstract class ShapeDecoratorCyclePolicy
{
    public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);

    public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
}

public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
{
    private bool Handler(Type type, IList<Type> allTypes)
    {
        if (allTypes.Contains(type))
        {
            throw new InvalidOperationException($"Cycle detected! Type is already a {type.FullName}!");
        }

        return true;
    }

    public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }

    public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }
}

public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
{
    public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
    {
        return true;
    }

    public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
    {
        return !allTypes.Contains(type);
    }
}

public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
{
    public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
    {
        return true;
    }

    public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
    {
        return true;
    }
}

public class ColoredShape : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
{
    private Shape shape;
    private string color;

    public ColoredShape(Shape shape, string color) : base(shape)
    {
        this.shape = shape;
        this.color = color;
    }

    public override string AsString()
    {
        var sb = new StringBuilder($"{shape.AsString()}");

        if (policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
            sb.Append($" has the color {color}");

        return sb.ToString();
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