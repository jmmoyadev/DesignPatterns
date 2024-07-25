using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.DecoratorPattern.DecoratorInDependencyInjection;

public interface IReportingService
{
    void Report();
}

public class ReportingService : IReportingService
{
    public void Report()
    {
        Console.WriteLine("Here is your report");
    }
}

public class ReportingServiceWithLogging : IReportingService
{
    private readonly IReportingService decorated;

    public ReportingServiceWithLogging(IReportingService decorated)
    {
        this.decorated = decorated;
    }

    public void Report()
    {
        Console.WriteLine("Commencing log...");
        decorated.Report();
        Console.WriteLine("Ending log...");
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<ReportingService>().Named<IReportingService>("reporting");

        builder.RegisterDecorator<IReportingService>(
                       (context, service) => new ReportingServiceWithLogging(service),
                       "reporting");

        using var scope = builder.Build();
        var r = scope.Resolve<IReportingService>();
        r.Report();
    }
}