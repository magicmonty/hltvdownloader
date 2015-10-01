using System;
using Xunit;
using Shouldly;

namespace Pagansoft.Aria2.Core
{
    public class GIDTests
    {
        GID _sut;
        const string TestGID1 = "0123456789ABCDEF";
        const string TestGID2 = "FEDCBA9876543210";

        public void SetUp()
        {
            _sut = new GID(TestGID1);
        }

        [Fact]
        public void ShouldBeEqualIfValueIsEqual()
        {
            SetUp();
            var other = new GID(TestGID1);
            _sut.Equals(other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeNotEqualIfValueIsNotEqual()
        {
            SetUp();
            var other = new GID(TestGID2);
            _sut.Equals(other).ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeNotEqualIfValueIsNull()
        {
            SetUp();
            GID other = null;
            _sut.Equals(other).ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeSameIfValueIsEqual()
        {
            SetUp();
            var other = new GID(TestGID1);
            (_sut == other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldNotBeSameIfValueIsNotEqual()
        {
            SetUp();
            var other = new GID(TestGID2);
            (_sut != other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldNotBeSameIfValueIsNull()
        {
            SetUp();
            GID other = null;
            (_sut != other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldNotBeSameIfBothValuesAreNull()
        {
            SetUp();
            GID other = null;
            _sut = null;
            (_sut != other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeSameIfValueIsMatchingString()
        {
            SetUp();
            GID other = TestGID1;
            (_sut == other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldNotBeSameIfValueIsNonMatchingString()
        {
            SetUp();
            GID other = TestGID2;
            (_sut != other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeSameIfValueIsRealString()
        {
            SetUp();
            string other = TestGID1;
            (_sut == other).ShouldBeTrue();
        }

        [Fact]
        public void ShouldThrowExceptionIfGidIsNotHex()
        {
            SetUp();
            Should.Throw<ArgumentException>(
                () => new GID("TEST"));
        }

        [Fact]
        public void ShouldThrowExceptionIfGidIsLongerThan16Chars()
        {
            SetUp();
            Should.Throw<ArgumentException>(
                () => new GID(TestGID1 + "12345"));
        }

        [Fact]
        public void ShouldThrowExceptionIfGidIsEmpty()
        {
            SetUp();
            Should.Throw<ArgumentException>(
                () => new GID(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionIfGidIsNull()
        {
            SetUp();
            Should.Throw<ArgumentException>(
                () => new GID(null));
        }

        [Fact]
        public void ShouldThrowExceptionIfGidIsWhitespace()
        {
            SetUp();
            Should.Throw<ArgumentException>(
                () => new GID(" "));
        }

        [Fact]
        public void ShouldReturnValuePaddedTo16CharsIfGidIsShorterThan16Chars()
        {
            SetUp();
            var sut = new GID("1");
            "0000000000000001".ShouldBe(sut.Value);
        }

        [Fact]
        public void PaddedAndUnpaddedGidsShouldBeEquivalent()
        {
            SetUp();
            ("1" == new GID("1")).ShouldBeTrue();
            (new GID("1") == "1").ShouldBeTrue();
        }

        [Fact]
        public void PaddedAndUnpaddedGidsShouldBeEqual()
        {
            SetUp();
            "1".ShouldBe(new GID("1"));
        }
    }
}

