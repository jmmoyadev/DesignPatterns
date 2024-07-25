using DesignPatterns.Structural.FacadePattern.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Tests;

public class MyVerifier
{
    public bool Verify(List<List<int>> array)
    {
        if (!array.Any()) return false;

        var rowCount = array.Count;
        var colCount = array[0].Count;

        var expected = array.First().Sum();

        for (var row = 0; row < rowCount; ++row)
            if (array[row].Sum() != expected)
                return false;

        for (var col = 0; col < colCount; ++col)
            if (array.Select(a => a[col]).Sum() != expected)
                return false;

        var diag1 = new List<int>();
        var diag2 = new List<int>();
        for (var r = 0; r < rowCount; ++r)
            for (var c = 0; c < colCount; ++c)
            {
                if (r == c)
                    diag1.Add(array[r][c]);
                var r2 = rowCount - r - 1;
                if (r2 == c)
                    diag2.Add(array[r][c]);
            }

        return diag1.Sum() == expected && diag2.Sum() == expected;
    }
}

[TestFixture]
public class FacadeTests
{
    private string SquareToString(List<List<int>> square)
    {
        var sb = new StringBuilder();
        foreach (var row in square)
        {
            sb.AppendLine(string.Join(" ",
              row.Select(x => x.ToString())));
        }

        return sb.ToString();
    }

    [Test]
    public void TestSizeThree()
    {
        var gen = new MagicSquareGenerator();
        var square = gen.Generate(3);

        Console.WriteLine(SquareToString(square));

        var v = new MyVerifier(); // prevents cheating :)
        Assert.IsTrue(v.Verify(square),
          "Verification failed: this is not a magic square");
    }
}