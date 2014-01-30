using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkIdModelTest
    {
        LinkIdModel _sut;
        List<LinkIdPersistenceModel> _linkIds;
        static readonly LinkIdPersistenceModel link1 = new LinkIdPersistenceModel("ABC", "1234", "http://test.de", "1");
        static readonly LinkIdPersistenceModel link2 = new LinkIdPersistenceModel("DEF", "5678", "http://example.com", "2");

        [SetUp]
        public void SetUp()
        {
            _linkIds = new List<LinkIdPersistenceModel>();

            var storage = new Mock<IStorage>();
            storage.Setup(s => s.LoadLinks()).Returns(_linkIds);
            storage.Setup(s => s.SaveLinks(It.IsAny<IEnumerable<LinkIdPersistenceModel>>()))
                   .Callback((IEnumerable<LinkIdPersistenceModel> list) => _linkIds = list.ToList());

            _sut = new LinkIdModel(storage.Object);
        }

        [Test]
        public void ShouldReturnEmtpyStringOnGetListIdByLinkIdIfListIsNotFound()
        {
            Assert.That(_sut.GetListIdByGid("3"), Is.EqualTo(string.Empty));
        }

        [Test]
        public void ShouldReturnCorrectListIdOnGetListIdByLinkIdIfListIsFound()
        {
            _linkIds.Add(link1);
            _linkIds.Add(link2);
            Assert.That(_sut.GetListIdByGid("1"), Is.EqualTo("ABC"));
            Assert.That(_sut.GetLinkIdByGid("1"), Is.EqualTo("1234"));
            Assert.That(_sut.GetListIdByGid("2"), Is.EqualTo("DEF"));
            Assert.That(_sut.GetLinkIdByGid("2"), Is.EqualTo("5678"));
        }

        [Test]
        public void ShouldAddLinkIdToListIfListAlreadyExists()
        {
            _linkIds.Add(link1);
            _sut.SaveLinkId(link2.LinkId, link2.ListId, link2.Url, link2.Gid);
            Assert.That(_linkIds.Count, Is.EqualTo(2));
            var secondLink = _linkIds.ElementAt(1);
            Assert.That(secondLink.Gid, Is.EqualTo(link2.Gid));
            Assert.That(secondLink.LinkId, Is.EqualTo(link2.LinkId));
            Assert.That(secondLink.ListId, Is.EqualTo(link2.ListId));
            Assert.That(secondLink.Url, Is.EqualTo(link2.Url));
        }

        [Test]
        public void ShouldRemoveNothingIfLinkIsNotInList()
        {
            _linkIds.Add(link1);
            _sut.RemoveLinkId(link2.Gid);
            Assert.That(_linkIds.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(new[] { link1 }, _linkIds);
        }

        [Test]
        public void ShouldNotAddLinkIfLinkIsAlreadyInList()
        {
            _linkIds.Add(link1);
            _sut.SaveLinkId(link1.LinkId, link1.ListId, link1.Url, link1.Gid);
            Assert.That(_linkIds.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(new[] { link1 }, _linkIds);
        }
    }
}

