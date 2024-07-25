using System.Text;

namespace DesignPatterns.Creational.BuilderPattern.Exercise;

public class CodeBuilder
{
    public string ClassName { get; set; }

    public Dictionary<string, string> Properties { get; set; }
        = new Dictionary<string, string>();

    public CodeBuilder(string className)
    {
        this.ClassName = className;
    }

    public CodeBuilder AddField(string fieldName, string type)
    {
        Properties.Add(fieldName, type);
        return this;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public class {this.ClassName}");
        sb.AppendLine("{");

        foreach (var prop in Properties)
        {
            sb.AppendLine($"  public {prop.Value} {prop.Key};");
        }

        sb.AppendLine("}");

        return sb.ToString();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var cb = new CodeBuilder("Person")
                    .AddField("Name", "string")
                    .AddField("Age", "int");
        Console.WriteLine(cb);
    }
}