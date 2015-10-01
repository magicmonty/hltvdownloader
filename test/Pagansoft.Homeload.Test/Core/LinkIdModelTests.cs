using System.Collections.Generic;
using System.Linq;
using Xunit;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    public class LinkIdModelTests
    {
        LinkIdModel _sut;
        List<LinkIdPersistenceModel> _linkIds;
        static readonly LinkIdPersistenceModel link1 = new LinkIdPersistenceModel("ABC", "1234", "http://test.de", "1");
        static readonly LinkIdPersistenceModel link2 = new LinkIdPersistenceModel("DEF", "5678", "http://example.com", "2");

        public void CreateSut()
        {
            _linkIds = new List<LinkIdPersistenceModel>();

            var storage = Mock.Create<IStorage>();
            storage.Arrange(s => s.LoadLinks()).Returns(_linkIds);
            storage.Arrange(s => s.SaveLinks(Arg.IsAny<IEnumerable<LinkIdPersistenceModel>>()))
                .DoInstead((IEnumerable<LinkIdPersistenceModel> list) => _linkIds = list.ToList());

            _sut = new LinkIdModel(storage);
        }

        [Fact]
        public void ShouldReturnEmtpyStringOnGetListIdByLinkIdIfListIsNotFound()
        {
            CreateSut();
            _sut.GetListIdByGid("3").ShouldBeEmpty();
        }

        [Fact]
        public void ShouldReturnCorrectListIdOnGetListIdByLinkIdIfListIsFound()
        {
            CreateSut();
            _linkIds.Add(link1);
            _linkIds.Add(link2);
            _sut.GetListIdByGid("1").ShouldBe("ABC");
            _sut.GetLinkIdByGid("1").ShouldBe("1234");
            _sut.GetListIdByGid("2").ShouldBe("DEF");
            _sut.GetLinkIdByGid("2").ShouldBe("5678");
        }

        [Fact]
        public void ShouldAddLinkIdToListIfListAlreadyExists()
        {
            _linkIds.Add(link1);
            _sut.SaveLinkId(link2.LinkId, link2.ListId, link2.Url, link2.Gid);
            _linkIds.Count.ShouldBe(2);
            var secondLink = _linkIds.ElementAt(1);
            secondLink.Gid.ShouldBe(link2.Gid);
            secondLink.LinkId.ShouldBe(link2.LinkId);
            secondLink.ListId.ShouldBe(link2.ListId);
            secondLink.Url.ShouldBe(link2.Url);
        }

        [Fact]
        public void ShouldRemoveNothingIfLinkIsNotInList()
        {
            _linkIds.Add(link1);
            _sut.RemoveLinkId(link2.Gid);
            _linkIds.ShouldBe(new[] { link1 });
        }

        [Fact]
        public void ShouldNotAddLinkIfLinkIsAlreadyInList()
        {
            _linkIds.Add(link1);
            _sut.SaveLinkId(link1.LinkId, link1.ListId, link1.Url, link1.Gid);
            _linkIds.ShouldBe(new[] { link1 });
        }
    }
}

