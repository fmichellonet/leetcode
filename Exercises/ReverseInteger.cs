using System.Linq;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class ReverseInteger
{
    public int Reverse(int x)
    {
        var reverted = new string(x.ToString().Reverse().ToArray());
        if (reverted.Last() == '-')
        {
            reverted = $"-{reverted.TrimEnd('-')}";
        }
        if (int.TryParse(reverted, out var res))
        {
            return res;
        }
        return 0;
    }

    [Test]
    public void Case1()
    {
        // Arrange
        int num = 123;
        int expected = 321;

        // Act
        var res = Reverse(num);

        // Assert
        Assert.That(res, Is.EqualTo(expected));
    }
}