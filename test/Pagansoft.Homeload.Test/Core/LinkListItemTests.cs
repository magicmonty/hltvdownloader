using System;
using Xunit;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    public class LinkListItemTests
    {
        [Fact]
        public void ShouldInitializeCorrectly()
        {
            var item = new LinkListItem(url: "http://www.google.de", id: "12345");
            item.Url.ShouldBe("http://www.google.de");
            item.Id.ShouldBe("12345");
        }

        [Fact]
        public void ShouldThrowExceptionIfUrlIsNull()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: null, id: "12345"));
        }

        [Fact]
        public void ShouldThrowExceptionIfUrlIsEmpty()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: string.Empty, id: "12345"));
        }

        [Fact]
        public void ShouldThrowExceptionIfUrlIsOnlyWhiteSpaces()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "  ", id: "12345"));
        }

        [Fact]
        public void ShouldThrowExceptionIfIdIsNull()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: null));
        }

        [Fact]
        public void ShouldThrowExceptionIfIdIsEmpty()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionIfIdIsOnlyWhiteSpaces()
        {
            Should.Throw<ArgumentException>(() => 
                new LinkListItem(url: "http://www.google.de", id: "  "));
        }

        [Fact]
        public void ShouldParseCorrectly()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi;11272262;");
            actual.Id.ShouldBe("11272262");
            actual.Url.ShouldBe("http://81.95.11.6/download/de/Sandmaennchen.avi");
        }

        [Fact]
        public void ShouldReturnNullOnParseError()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi");
            actual.ShouldBeNull();
        }
    }
}

