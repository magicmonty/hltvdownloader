using System;
using Shouldly;
using NUnit.Framework;

namespace Pagansoft.Aria2.Core
{
    [TestFixture]
    public class GIDTests
    {
        const string TestGID1 = "0123456789ABCDEF";
        const string TestGID2 = "FEDCBA9876543210";

        [Test]
        public void ShouldBeEqualIfValueIsEqual()
        {
            var sut = new GID(TestGID1);
            var other = new GID(TestGID1);

            sut.Equals(other).ShouldBeTrue();
        }

        [Test]
        public void ShouldBeNotEqualIfValueIsNotEqual()
        {
            var sut = new GID(TestGID1);
            var other = new GID(TestGID2);

            sut.Equals(other).ShouldBeFalse();
        }

        [Test]
        public void ShouldBeNotEqualIfValueIsNull()
        {
            var sut = new GID(TestGID1);
            GID other = null;
            sut.Equals(other).ShouldBeFalse();
        }

        [Test]
        public void ShouldBeSameIfValueIsEqual()
        {
            var sut = new GID(TestGID1);
            var other = new GID(TestGID1);
            (sut == other).ShouldBeTrue();
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNotEqual()
        {
            var sut = new GID(TestGID1);
            var other = new GID(TestGID2);
            (sut != other).ShouldBeTrue();
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNull()
        {
            var sut = new GID(TestGID1);
            GID other = null;
            (sut != other).ShouldBeTrue();
        }

        [Test]
        public void ShouldNotBeSameIfBothValuesAreNull()
        {
            GID other = null;
            GID sut = null;
            (sut == other).ShouldBeTrue();
        }

        [Test]
        public void ShouldBeSameIfValueIsMatchingString()
        {
            var sut = new GID(TestGID1);
            GID other = TestGID1;
            (sut == other).ShouldBeTrue();
        }

        [Test]
        public void ShouldNotBeSameIfValueIsNonMatchingString()
        {
            var sut = new GID(TestGID1);
            GID other = TestGID2;
            (sut != other).ShouldBeTrue();
        }

        [Test]
        public void ShouldBeSameIfValueIsRealString()
        {
            var sut = new GID(TestGID1);
            string other = TestGID1;
            (sut == other).ShouldBeTrue();
            (other == sut).ShouldBeTrue();
        }

        [Test]
        public void ShouldThrowExceptionIfGidIsNotHex()
        {
            Should.Throw<ArgumentException>(
                () => new GID("TEST"));
        }

        [Test]
        public void ShouldThrowExceptionIfGidIsLongerThan16Chars()
        {
            Should.Throw<ArgumentException>(
                () => new GID(TestGID1 + "12345"));
        }

        [Test]
        public void ShouldThrowExceptionIfGidIsEmpty()
        {
            Should.Throw<ArgumentException>(
                () => new GID(string.Empty));
        }

        [Test]
        public void ShouldThrowExceptionIfGidIsNull()
        {
            Should.Throw<ArgumentException>(
                () => new GID(null));
        }

        [Test]
        public void ShouldThrowExceptionIfGidIsWhitespace()
        {
            Should.Throw<ArgumentException>(
                () => new GID(" "));
        }

        [Test]
        public void ShouldReturnValuePaddedTo16CharsIfGidIsShorterThan16Chars()
        {
            var sut = new GID("1");
            "0000000000000001".ShouldBe(sut.Value);
        }

        [Test]
        public void PaddedAndUnpaddedGidsShouldBeEquivalent()
        {
            ("1" == new GID("1")).ShouldBeTrue();
            (new GID("1") == "1").ShouldBeTrue();
        }

        [Test]
        public void PaddedAndUnpaddedGidsShouldBeEqual()
        {
            new GID("1").Equals("1").ShouldBeTrue();
        }
    }
}

