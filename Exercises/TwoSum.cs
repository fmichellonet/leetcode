using System.Linq;
using NUnit.Framework;

namespace Exercises;

[TestFixture]
public class TwoSum
{
    public int[] Compute(int[] nums, int target)
    {
        var uniqueValues = nums.ToHashSet();
        var indexesPerValue = nums.Select((val, idx) => new { val, idx })
            .GroupBy(x => x.val)
            .ToDictionary(x => x.Key, x => x.ToList().Select(y => y.idx));

        for (var index = 0; index < nums.Length; index++)
        {
            var current = nums[index];
            var complementary = target - current;
            if (!uniqueValues.Contains(complementary))
            {
                continue;
            }
                
            var otherVal = indexesPerValue[complementary]
                .Cast<int?>()
                .FirstOrDefault(x => x != index);

            if (otherVal == null)
            {
                continue;
            }
            return [index, otherVal.Value];
        }

        return [];
    }

    [Test]
    public void Case1()
    {
        // Arrange
        int[] nums = [2, 7, 11, 15];
        int[] expected = [0, 1];

        // Act
        var res = Compute(nums, 9);
            
        // Assert
        Assert.That(res, Is.EquivalentTo(expected));
    }

    [Test]
    public void Case2()
    {
        // Arrange
        int[] nums = [3, 2, 4];
        int[] expected = [1, 2];

        // Act
        var res = Compute(nums, 6);

        // Assert
        Assert.That(res, Is.EquivalentTo(expected));
    }

    [Test]
    public void Case3()
    {
        // Arrange
        int[] nums = [3, 3];
        int[] expected = [0, 1];

        // Act
        var res = Compute(nums, 6);

        // Assert
        Assert.That(res, Is.EquivalentTo(expected));
    }
}