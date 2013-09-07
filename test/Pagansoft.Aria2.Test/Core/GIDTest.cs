using NUnit.Framework;
using System;

namespace Pagansoft.Aria2.Core
{
    [TestFixture]
    public class GIDTest
    {
        private GID _sut;
        private const string TestGID1 = "0123456789ABCDEF";
        private const string TestGID2 = "FEDCBA9876543210";

        [SetUp]
        public void SetUp()
        {
            _sut = new Core.GID(TestGID1);
        }

        [Test]
        public void ShouldBeEqualIfValueIsEqual()
        {
            var other = new Core.GID(TestGID1);
            Assert.That(_sut.Equals(other), Is.True);
        }

        [Test]
        public void ShouldBeNotEqualIfValueIsNotEqual()
        {
            var other = new Core.GID(TestGID2);
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
            var other = new Core.GID(TestGID1);
            Assert.That(_sut == other, Is.True);
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNotEqual()
        {
            var other = new Core.GID(TestGID2);
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
            Assert.That(_sut != other, Is.True);
        }

        [Test]
        public void ShouldBeSameIfValueIsMatchingString()
        {
            Core.GID other = TestGID1;
            Assert.That(_sut == other, Is.True);
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNonMatchingString()
        {
            Core.GID other = TestGID2;
            Assert.That(_sut != other, Is.True);
        }

        [Test]
        public void ShouldBeSameIfValueIsRealString()
        {
            string other = TestGID1;
            Assert.That(_sut == other, Is.True);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfGidIsNotHex()
        {
            new GID("TEST");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfGidIsLongerThan16Chars()
        {
            new GID(TestGID1 + "12345");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfGidIsEmpty()
        {
            new GID(string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfGidIsNull()
        {
            new GID(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfGidIsWhitespace()
        {
            new GID(" ");
        }

        [Test]
        public void ShouldReturnValuePaddedTo16CharsIfGidIsShorterThan16Chars()
        {
            var sut = new GID("1");
            Assert.That("0000000000000001", Is.EqualTo(sut.Value));
        }

        [Test]
        public void PaddedAndUnpaddedGidsShouldBeEquivalent()
        {
            Assert.That("1" == new GID("1"), Is.True);
            Assert.That(new GID("1") == "1", Is.True);
        }

        [Test]
        public void PaddedAndUnpaddedGidsShouldBeEqual()
        {
            Assert.That("1", Is.EqualTo(new GID("1")));
        }
    }
}

