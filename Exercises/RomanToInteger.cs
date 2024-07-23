using System.Collections.Generic;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class RomanToInteger
{
    private static readonly Dictionary<string, int> Lookup = new()
    {
        {"I", 1},
        {"IV", 4},
        {"V", 5},
        {"IX", 9},
        {"X", 10 },
        {"XL", 40 },
        {"L", 50 },
        {"XC", 90 },
        {"C", 100 },
        {"CD", 400 },
        {"D", 500 },
        {"CM", 900 },
        {"M", 1000}
    };

    public int RomanToInt(string s)
    {
        var sum = 0;

        for (var idx = 0; idx < s.Length; idx++)
        {
            int val;
            if (idx + 1 < s.Length)
            {
                if (Lookup.TryGetValue(new string(new[] { s[idx], s[idx+1] }), out val))
                {
                    sum += val;
                    idx++;
                    continue;
                }
            }
            Lookup.TryGetValue(new string(new[] { s[idx]}), out val);
            sum += val;
        }
        return sum;
    }


    [Test]
    public void Case1()
    {
        // Act
        var res = RomanToInt("X");

        // Assert
        Assert.That(res, Is.EqualTo(10));
    }

    [Test]
    public void Case2()
    {
        // Act
        var res = RomanToInt("IV");

        // Assert
        Assert.That(res, Is.EqualTo(4));
    }

    [Test]
    public void Case3()
    {
        // Act
        var res = RomanToInt("III");

        // Assert
        Assert.That(res, Is.EqualTo(3));
    }
}