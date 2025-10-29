
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace HomeExercise.Tasks.NumberValidator;

[TestFixture]
public class NumberValidatorTests
{
    [Test]
    [TestCase(-1, 2, true)]
    [TestCase(1, -1, true)]
    [TestCase(1, 1, true)]
    [TestCase(-1, 2, false)]
    [TestCase(1, -1, false)]
    [TestCase(1, 1, false)]
    public void ThrowArgumentException_AfterCallingConstructor(int precision, int scale, bool onlyPositive)
    {
        var action = () => new NumberValidator(precision, scale, onlyPositive);
        
        action.Should().Throw<ArgumentException>();
    }
    
    [Test]
    [TestCase(1, 0, true)]
    [TestCase(1, 0, false)]
    public void NotThrowException_AfterCallingConstructor(int precision, int scale, bool onlyPositive)
    {
        var action = () => new NumberValidator(precision, scale, onlyPositive);
        
        action.Should().NotThrow();
    }
    
    [Test]
    [TestCase(17, 2, true, "0.0")]
    [TestCase(17, 2, true, "0")]
    [TestCase(3, 2, false, "-00")]
    [TestCase(3, 2, false, "-0.0")]
    [TestCase(4, 2, true, "+1.23")]
    [TestCase(4, 2, true, "+12")]
    public void IsTrue_AfterCallingIsValidNumber(int precision, int scale, bool onlyPositive, string number)
    {
        var result = new NumberValidator(precision, scale, onlyPositive).IsValidNumber(number);

        result.Should().Be(true);
    }
    
    [Test]
    [TestCase(3, 2, true, "0000")]
    [TestCase(3, 2, true, "+000")]
    [TestCase(3, 2, true, "-00")]
    [TestCase(3, 2, true, "00.00")]
    [TestCase(3, 2, true, "+0.00")]
    [TestCase(3, 2, true, "-0.0")]
    [TestCase(17, 2, true, "0.000")]
    [TestCase(3, 2, true, "a.sd")]
    [TestCase(20, 2, true, "++123")]
    public void IsFalse_AfterCallingIsValidNumber(int precision, int scale, bool onlyPositive, string number)
    {
        var result = new NumberValidator(precision, scale, onlyPositive).IsValidNumber(number);

        result.Should().Be(false);
    }
}