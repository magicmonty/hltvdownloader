using NUnit.Framework;
using Core = PaganSoft.Aria2.Core;
using NUnit.Framework.SyntaxHelpers;
 
namespace PaganSoft.Aria2.Test
{
    [TestFixture]
    public class GID
    {
        private Core.GID _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Core.GID("TEST");
        }

        [Test]
        public void ShouldBeEqualIfValueIsEqual()
        {
            var other = new Core.GID("TEST");
            Assert.That(_sut.Equals(other), Is.True);
        }

        [Test]
        public void ShouldBeNotEqualIfValueIsNotEqual()
        {
            var other = new Core.GID("TEST1");
            Assert.That(_sut.Equals(other), Is.False);
        }

        [Test]
        public void ShouldBeNotEqualIfValueIsNull()
        {
            Core.GID other = null;
            Assert.That(_sut.Equals(other), Is.False);
        }

        [Test]
        public void ShouldBeSameIfValueIsEqual()
        {
            var other = new Core.GID("TEST");
            Assert.That(_sut == other, Is.True);
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNotEqual()
        {
            var other = new Core.GID("TEST1");
            Assert.That(_sut != other, Is.True);
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNull()
        {
            Core.GID other = null;
            Assert.That(_sut != other, Is.True);
        }
                
        [Test]
        public void ShouldNotBeSameIfBothValuesAreNull()
        {
            Core.GID other = null;
            _sut = null;
            Assert.IsTrue(_sut != other);
        }
        
        [Test]
        public void ShouldBeSameIfValueIsMatchingString()
        {
            Core.GID other = "TEST";
            Assert.IsTrue(_sut == other);
        }
        
        [Test]
        public void ShouldNotBeSameIfValueIsNonMatchingString()
        {
            Core.GID other = "TEST1";
            Assert.IsTrue(_sut != other);
        }
        
        [Test]
        public void ShouldBeSameIfValueIsRealString()
        {
            string other = "TEST";
            Assert.IsTrue(_sut == other);
        }
    }
}

