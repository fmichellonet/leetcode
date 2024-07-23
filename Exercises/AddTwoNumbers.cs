using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class AddTwoNumbers
{
    public ListNode Compute(ListNode l1, ListNode l2)
    {
        var l1Str = Read(l1);
        var l2Str = Read(l2);
        var longer = l1Str.Length > l2Str.Length ? l1Str : l2Str;
        var shorter = l1Str.Length <= l2Str.Length ? l1Str : l2Str;
        var sumStr = Sum(longer, shorter);
        return AsListNode(sumStr);
    }

    private static string Sum(string longer, string shorter)
    {
        var carry = 0;
        var res = new List<char>();
        for (var index = 0; index < longer.Length; index++)
        {
            var longerCar = longer[index];
            var shorterCar = '0';

            if (index < shorter.Length)
            {
                shorterCar = shorter[index];
            }

            var val = int.Parse(longerCar.ToString()) + int.Parse(shorterCar.ToString()) + carry;
            res.Add(val.ToString().Last());
            carry = val > 9 ? 1 : 0;
        }

        if (carry == 1)
        {
            res.Add('1');
        }

        return res.Count == 0 ? string.Empty : new string(res.ToArray());
    }

    private static ListNode AsListNode(string value)
    {
        var first = new ListNode(int.Parse(value.First().ToString()));
        var previous = first;
        foreach (var car in value.Skip(1))
        {
            var node = new ListNode(int.Parse(car.ToString()));
            previous.next = node;
            previous = node;
        }
        return first;
    }
    
    private static string Read(ListNode l1)
    {
        var node = l1;
        var val = string.Empty;
        while (node != null)
        {
            val += node.val.ToString();
            node = node.next;
        }
        return val;
    }

    [Test]
    public void Case1()
    {
        // Arrange
        var l1 = new ListNode(2, new ListNode(4, new ListNode(3)));
        var l2 = new ListNode(5, new ListNode(6, new ListNode(4)));
        var expected = new ListNode(7, new ListNode(0, new ListNode(8)));

        // Act
        var res = Compute(l1, l2);

        // Assert
        Assert.That(res, Is.EqualTo(expected));
    }

    [Test]
    public void Case2()
    {
        // Arrange
        var l1 = new ListNode(9);
        var l2 = new ListNode(1,
            new ListNode(9,
                new ListNode(9,
                    new ListNode(9,
                        new ListNode(9,
                            new ListNode(9,
                                new ListNode(9,
                                    new ListNode(9,
                                        new ListNode(9,
                                            new ListNode(9))))))))));
        //10000000000

        var expected = new ListNode(0,
            new ListNode(0,
                new ListNode(0,
                    new ListNode(0,
                        new ListNode(0,
                            new ListNode(0,
                                new ListNode(0,
                                    new ListNode(0,
                                        new ListNode(0,
                                            new ListNode(0,
                                                new ListNode(1)))))))))));

        // Act
        var res = Compute(l1, l2);

        // Assert
        Assert.That(res, Is.EqualTo(expected));
    }
}

public class ListNode : IEquatable<ListNode>
{
    public bool Equals(ListNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return val == other.val && Equals(next, other.next);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ListNode)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(val, next);
    }

    
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}
