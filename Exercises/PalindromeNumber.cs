using System;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class PalindromeNumber
{
    public bool IsPalindrome(int x)
    {
        var str = x.ToString().AsSpan();
        for (int i = 0, j = str.Length -1; i < str.Length; i++, j--)
        {
            if (str[i] != str[j])
                return false;
        }
        return true;
    }

    [Test]
    public void Case1()
    {
        // Act
        var res = IsPalindrome(121);

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case2()
    {
        // Act
        var res = IsPalindrome(-121);

        // Assert
        Assert.That(res, Is.False);
    }
}