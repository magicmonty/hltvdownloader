using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkIdModelTest
    {
        private LinkIdModel _sut;
        private IDictionary<string, IEnumerable<string>> _linkIds;

        [SetUp]
        public void SetUp()
        {
            _linkIds = new Dictionary<string, IEnumerable<string>>();

            var storage = new Mock<IStorage>();
            storage.Setup(s => s.LoadLinks()).Returns(_linkIds);
            storage.Setup(s => s.SaveLinks(It.IsAny<IDictionary<string, IEnumerable<string>>>()))
                   .Callback((IDictionary<string, IEnumerable<string>> list) => _linkIds = list);

            _sut = new LinkIdModel(storage.Object);
        }

        [Test]
        public void ShouldReturnEmtpyStringOnGetListIdByLinkIdIfListIsNotFound()
        {
            Assert.That(_sut.GetListIdByLinkId("1234"), Is.EqualTo(string.Empty));
        }

        [Test]
        public void ShouldReturnCorrectListIdOnGetListIdByLinkIdIfListIsFound()
        {
            _linkIds.Add("ABC", new [] { "1234" });
            _linkIds.Add("DEF", new [] { "567" });
            Assert.That(_sut.GetListIdByLinkId("1234"), Is.EqualTo("ABC"));
        }

        [Test]
        public void ShouldAddLinkIdToListIfListAlreadyExists()
        {
            _linkIds.Add("ABC", new [] { "1234" });
            _sut.SaveLinkId("5678", "ABC");
            CollectionAssert.AreEquivalent(new[] { "1234", "5678" }, _linkIds["ABC"]);
        }

        [Test]
        public void ShouldNotAddLinkIdToListIfLinkInListAlreadyExists()
        {
            _linkIds.Add("ABC", new [] { "1234" });
            _sut.SaveLinkId("1234", "ABC");
            CollectionAssert.AreEquivalent(new[] { "1234" }, _linkIds["ABC"]);
        }

        [Test]
        public void ShouldAddListWithLinkIdToListDoesNotExist()
        {
            _sut.SaveLinkId("1234", "ABC");
            CollectionAssert.AreEquivalent(new[] { "1234" }, _linkIds["ABC"]);
        }

        [Test]
        public void ShouldRemoveLinkFromListWithIfItExists()
        {
            _linkIds.Add("ABC", new [] { "1234", "5678" });
            _sut.RemoveLinkId("1234");
            CollectionAssert.AreEquivalent(new[] { "5678" }, _linkIds["ABC"]);
        }

        [Test]
        public void ShouldRemoveListOnRemoveOfLastLinkInList()
        {
            _linkIds.Add("ABC", new [] { "1234" });
            _sut.RemoveLinkId("1234");
            Assert.That(_linkIds.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldRemoveNothingIfLinkIsNotInList()
        {
            _linkIds.Add("ABC", new [] { "1234" });
            _sut.RemoveLinkId("5678");
            Assert.That(_linkIds.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(new[] { "1234" }, _linkIds["ABC"]);
        }
    }
}

