using System;
using NUnit.Framework;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkListItemTests
    {
        [Test]
        public void ShouldInitializeCorrectly()
        {
            var item = new LinkListItem(url: "http://www.google.de", id: "12345");
            item.Url.ShouldBe("http://www.google.de");
            item.Id.ShouldBe("12345");
        }

        [Test]
        public void ShouldThrowExceptionIfUrlIsNull()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: null, id: "12345"));
        }

        [Test]
        public void ShouldThrowExceptionIfUrlIsEmpty()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: string.Empty, id: "12345"));
        }

        [Test]
        public void ShouldThrowExceptionIfUrlIsOnlyWhiteSpaces()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "  ", id: "12345"));
        }

        [Test]
        public void ShouldThrowExceptionIfIdIsNull()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: null));
        }

        [Test]
        public void ShouldThrowExceptionIfIdIsEmpty()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: string.Empty));
        }

        [Test]
        public void ShouldThrowExceptionIfIdIsOnlyWhiteSpaces()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: "  "));
        }

        [Test]
        public void ShouldParseCorrectly()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi;11272262;");
            actual.Id.ShouldBe("11272262");
            actual.Url.ShouldBe("http://81.95.11.6/download/de/Sandmaennchen.avi");
        }

        [Test]
        public void ShouldReturnNullOnParseError()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi");
            actual.ShouldBeNull();
        }
    }
}

