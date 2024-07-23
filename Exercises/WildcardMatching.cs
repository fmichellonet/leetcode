using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class WildcardMatching
{
    private const char Asterisk = '*';

    public bool IsMatch(string s, string p)
    {
        return IsMatch(s.AsSpan(), p.AsSpan(), 0);
    }

    internal record WildcardingKey(string Sentence, string Pattern);

    private readonly Dictionary<WildcardingKey, bool> _memo = [];

    public bool IsMatch(ReadOnlySpan<char> s, ReadOnlySpan<char> p, int eager)
    {
        var key = new WildcardingKey(s.ToString(), p.ToString());
        if (p.Length == 0 && s.Length == 0)
        {
            _memo.TryAdd(key, true);
            return true;
        }

        if (p.Length == 0 && s.Length > 0)
        {
            _memo.TryAdd(key, false);
            return false;
        }

        if (s.Length == 0 && !p.ContainsAnyExcept(Asterisk))
        {
            _memo.TryAdd(key, true);
            return true;
        }

        if (s.Length == 0 && p.ContainsAnyExcept(Asterisk))
        {
            _memo.TryAdd(key, false);
            return false;
        }

        if (s.Length == 0 && (p.Length == 0 || p is [Asterisk]))
        {
            _memo.TryAdd(key, true);
            return true;
        }

        if (_memo.TryGetValue(key, out var existingResult))
        {
            return existingResult;
        }

        switch (p[0])
        {
            case '?':
                return IsMatch(s[1..], p[1..], eager);
            case Asterisk:
                if (eager == 0 && MatchEmptySequence(s, p))
                {
                    _memo.TryAdd(key, true);
                    return true;
                }

                if (eager > 0 && CanRealign(s, p))
                {
                    var res = IsMatch(s, p[1..], 0);
                    _memo.TryAdd(key, res);
                    if (res)
                    {
                        return true;
                    }
                }
                return IsMatch(s[1..], p, ++eager);
            default:
                {
                    if (s[0] != p[0])
                    {
                        _memo.TryAdd(key, false);
                        return false;
                    }

                    return IsMatch(s[1..], p[1..], eager);
                }
        }
    }

    private static bool CanRealign(ReadOnlySpan<char> s, ReadOnlySpan<char> p)
    {
        if (p.Length < 2)
        {
            return false;
        }

        if (p[1] == Asterisk)
        {
            return CanRealign(s, p[1..]);
        }

        return s[0] == p[1] || p[1] == '?';
    }

    private bool MatchEmptySequence(ReadOnlySpan<char> s, ReadOnlySpan<char> p)
    {
        return IsMatch(s, p[1..], 1);
    }

    [Test]
    public void Case1()
    {
        // Act
        var res = IsMatch("aa", "aa");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case2()
    {
        // Act
        var res = IsMatch("aa", "a");

        // Assert
        Assert.That(res, Is.False);
    }

    [Test]
    public void Case3()
    {
        // Act
        var res = IsMatch("acdcb", "a*c?b");

        // Assert
        Assert.That(res, Is.False);
    }

    [Test]
    public void Case4()
    {
        // Act
        var res = IsMatch("aa", "*");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case5()
    {
        // Act
        var res = IsMatch("adceb", "*a*b");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case6()
    {
        // Act
        var res = IsMatch("", "******");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case7()
    {
        // Act
        var res = IsMatch("abefcdgiescdfimde", "ab*cd?i*de");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case8()
    {
        // Act
        var res = IsMatch("hi", "*?");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case9()
    {
        // Act
        var res = IsMatch("abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb", "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb");

        // Assert
        Assert.That(res, Is.False);
    }

    [Test]
    public void Case10()
    {
        // Act
        var res = IsMatch("aaaa", "***a");

        // Assert
        Assert.That(res, Is.True);
    }

    [Test]
    public void Case11()
    {
        // Act
        var res = IsMatch("bbbbbbbabbaabbabbbbaaabbabbabaaabbababbbabbbabaaabaab", "b*b*ab**ba*b**b***bba");

        // Assert
        Assert.That(res, Is.False);
    }
}