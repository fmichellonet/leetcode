using System.Collections.Generic;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class ValidParentheses
{
    public bool IsValid(string s)
    {
        var stack = new Stack<char>();
        foreach (var c in s)
        {
            if (IsOpening(c))
            {
                stack.Push(c);
            }
            else
            {
                if (!stack.TryPop(out var previousOpening))
                {
                    return false;
                }
                
                if (!IsValidClosing(c, previousOpening))
                {
                    return false;
                }
            }
        }
        return stack.Count == 0;
        
    }

    private static bool IsValidClosing(char closing, char previousOpening) =>
        previousOpening switch
        {
            '(' => closing == ')',
            '{' => closing == '}',
            '[' => closing == ']',
            _ => false
        };

    private static bool IsOpening(char c) => c is '(' or '{' or '[';


    [Test]
    public void Case1()
    {
        // Arrange
        var input = "([)]";
        var expected = false;

        // Act
        var res = IsValid(input);

        // Assert
        Assert.That(res, Is.EqualTo(expected));
    }

    [Test]
    public void Case2()
    {
        // Arrange
        var input = "]";
        var expected = false;

        // Act
        var res = IsValid(input);

        // Assert
        Assert.That(res, Is.EqualTo(expected));
    }
}