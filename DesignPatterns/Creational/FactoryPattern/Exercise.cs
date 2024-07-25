using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.FactoryPattern.Exercise;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PersonFactory
{
    private int cont = 0;

    public Person CreatePerson(string name)
    {
        return new Person()
        {
            Id = cont++,
            Name = name
        };
    }
}