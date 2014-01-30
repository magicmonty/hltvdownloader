using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkListTest
    {
        LinkList _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LinkList();
        }

        [Test]
        public void ShouldBeInitializedCorrectly()
        {
            CollectionAssert.AreEquivalent(Enumerable.Empty<LinkListItem>(), _sut);
            Assert.That(_sut.Id, Is.EqualTo(string.Empty), "Id");
            Assert.That(_sut.HappyHourStart, Is.Null, "HappyHourStart");
            Assert.That(_sut.HappyHourEnd, Is.Null, "HappyHourEnd");
            Assert.That(_sut.Error, Is.EqualTo(string.Empty), "Error");
            Assert.That(_sut.NumberOfLinks, Is.EqualTo(0), "NumberOfLinks");
            Assert.That(_sut.LinkCount, Is.EqualTo(0), "LinkCount");
            Assert.That(_sut.Interval, Is.EqualTo(0), "Interval");
        }

        [Test]
        public void ShouldReturnEmptyListWithErrorOnParseIfValueContainsError()
        {
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("USER_NOT_FOUND");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("WRONG_PASSWORD");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("DB_ERROR");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("NOT_ALLOWED");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("NO_NEW_LINKS");
        }

        public void ShouldReturnEmptyListWithErrorOnParseIfValueContainsError(string response)
        {
            var actual = LinkList.Parse(response);

            Assert.That(actual.LinkCount, Is.EqualTo(0));
            Assert.That(actual.Error, Is.EqualTo(response));
        }

        [Test]
        public void ShouldParseCorrectly()
        {
            var response = @"INTERVAL=60;NUMBER_OF_LINKS=1;LIST=3828;LINKCOUNT=2;HHSTART=0;HHEND=8;
http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi;11272262;
http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi;11274936;";

            var actual = LinkList.Parse(response);
            Assert.That(actual.Interval, Is.EqualTo(60), "Interval");
            Assert.That(actual.NumberOfLinks, Is.EqualTo(1), "NumberOfLinks");
            Assert.That(actual.Id, Is.EqualTo("3828"), "Id");
            Assert.That(actual.HappyHourStart, Is.EqualTo(0), "HappyHourStart");
            Assert.That(actual.HappyHourEnd, Is.EqualTo(8), "HappyHourEnd");

            Assert.That(actual.LinkCount, Is.EqualTo(2), "LinkCount");

            var link1 = actual.First();
            var link2 = actual.Last();

            Assert.That(link1.Url, Is.EqualTo("http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi"), "Link1 URL");
            Assert.That(link1.Id, Is.EqualTo("11272262"), "Link1 ID");
            Assert.That(link2.Url, Is.EqualTo("http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi"), "Link2 URL");
            Assert.That(link2.Id, Is.EqualTo("11274936"), "Link2 ID");
        }

        [Test]
        public void ShouldParseEmptyListCorrectly()
        {
            var response = @"INTERVAL=60;NUMBER_OF_LINKS=0;LIST=0;LINKCOUNT=0;HHSTART=0;HHEND=8;";

            var actual = LinkList.Parse(response);
            Assert.That(actual.Interval, Is.EqualTo(60), "Interval");
            Assert.That(actual.NumberOfLinks, Is.EqualTo(0), "NumberOfLinks");
            Assert.That(actual.Id, Is.EqualTo("0"), "Id");
            Assert.That(actual.HappyHourStart, Is.EqualTo(0), "HappyHourStart");
            Assert.That(actual.HappyHourEnd, Is.EqualTo(8), "HappyHourEnd");
            Assert.That(actual.LinkCount, Is.EqualTo(0), "LinkCount");
        }
    }
}

