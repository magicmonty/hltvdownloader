using System;
using NUnit.Framework;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkListItemTest
    {
        [Test]
        public void ShouldInitializeCorrectly()
        {
            var item = new LinkListItem(url: "http://www.google.de", id: "12345");
            Assert.That(item.Url, Is.EqualTo("http://www.google.de"));
            Assert.That(item.Id, Is.EqualTo("12345"));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfUrlIsNull()
        {
            new LinkListItem(url: null, id: "12345");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfUrlIsEmpty()
        {
            new LinkListItem(url: string.Empty, id: "12345");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfUrlIsOnlyWhiteSpaces()
        {
            new LinkListItem(url: "  ", id: "12345");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfIdIsNull()
        {
            new LinkListItem(url: "http://www.google.de", id: null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfIdIsEmpty()
        {
            new LinkListItem(url: "http://www.google.de", id: string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfIdIsOnlyWhiteSpaces()
        {
            new LinkListItem(url: "http://www.google.de", id: "  ");
        }

        [Test]
        public void ShouldParseCorrectly()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi;11272262;");
            Assert.That(actual.Id, Is.EqualTo("11272262"));
            Assert.That(actual.Url, Is.EqualTo("http://81.95.11.6/download/de/Sandmaennchen.avi"));
        }

        [Test]
        public void ShouldReturnNullOnParseError()
        {
            var actual = LinkListItem.Parse("http://81.95.11.6/download/de/Sandmaennchen.avi");
            Assert.That(actual, Is.Null);
        }
    }
}

