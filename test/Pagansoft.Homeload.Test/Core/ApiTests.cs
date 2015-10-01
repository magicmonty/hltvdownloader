using System.Linq;
using Xunit;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    public class ApiTests
    {
        const string LinkListResponse = @"INTERVAL=60;NUMBER_OF_LINKS=1;LIST=3828;LINKCOUNT=2;HHSTART=0;HHEND=8;
http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi;11272262;
http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi;11274936;";
        const string GetLinksUrl = "http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99";
        const string GetLinksUrlProcToNew = "http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true";
        const string SetStateUrl = "http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=linkId&state=finished&error=";
        const string SetErrorUrl = "http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=linkId&state=finished&error=brokenonopen";
        const string SetProcessingUrl = "http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=listId&state=processing";
        Api _sut;
        IHLTCHttpService _httpService;
        IConfiguration _configuration;

        private void CreateSut()
        {
            _httpService = Mock.Create<IHLTCHttpService>();
            _configuration = Mock.Create<IConfiguration>();
            _configuration.Arrange(c => c.HltvUserName).Returns("user");
            _configuration.Arrange(c => c.HltvPassword).Returns("password");

            _sut = new Api(_httpService, new UrlBuilder(_configuration));
        }

        [Fact]
        public void ShouldReturnLinkListOnSuccessfulGetInitialLinks()
        {
            CreateSut();
            _httpService.Arrange(h => h.SendGetRequest(GetLinksUrlProcToNew))
                        .Returns(LinkListResponse)
                        .OccursOnce();

            var task = _sut.GetLinks(initial: true);
            task.Wait();
            var actual = task.Result;

            _httpService.Assert();

            VerifyLinkList(actual);
        }

        static void VerifyLinkList(LinkList actual)
        {
            actual.ShouldSatisfyAllConditions(
                () => actual.Interval.ShouldBe(60, "Interval"),
                () => actual.NumberOfLinks.ShouldBe(1, "NumberOfLinks"),
                () => actual.Id.ShouldBe("3828", "Id"),
                () => actual.HappyHourStart.ShouldBe(0, "HappyHourStart"),
                () => actual.HappyHourEnd.ShouldBe(8, "HappyHourEnd"),
                () => actual.LinkCount.ShouldBe(2, "LinkCount"));

            var link1 = actual.First();
            link1.ShouldSatisfyAllConditions(
                () => link1.Url.ShouldBe("http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi", "Link1 URL"),
                () => link1.Id.ShouldBe("11272262", "Link1 ID"));

            var link2 = actual.Last();
            link2.ShouldSatisfyAllConditions(
                () => link2.Url.ShouldBe("http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi", "Link2 URL"),
                () => link2.Id.ShouldBe("11274936", "Link2 ID"));
        }

        [Fact]
        public void ShouldReturnLinkListOnSuccessfulGetLinks()
        {
            CreateSut();

            _httpService
                .Arrange(h => h.SendGetRequest(GetLinksUrl))
                .Returns(LinkListResponse)
                .OccursOnce();

            var task = _sut.GetLinks();
            task.Wait();
            var actual = task.Result;

            _httpService.Assert();

            VerifyLinkList(actual);
        }

        [Fact]
        public void ShouldExecuteSetStateInHttpServiceIfSetStateIsTriggered()
        {
            CreateSut();

            _httpService
                .Arrange(h => h.SendGetRequest(SetStateUrl))
                .Returns("OK")
                .OccursOnce();

            var task = _sut.SetState("linkId", LinkState.Finished);

            task.Wait();

            var actual = task.Result;
            _httpService.Assert();
            actual.ShouldBeTrue();
        }

        [Fact]
        public void ShouldExecuteSetErrorInHttpServiceIfSetErrorIsTriggered()
        {
            CreateSut();

            _httpService
                .Arrange(h => h.SendGetRequest(SetErrorUrl))
                .Returns("OK")
                .OccursOnce();

            var task = _sut.SetError("linkId");

            task.Wait();

            var actual = task.Result;
            _httpService.Assert();
            actual.ShouldBeTrue();
        }

        [Fact]
        public void ShouldExecuteSetProcessingInHttpServiceIfSetProcessingIsTriggered()
        {
            CreateSut();

            _httpService.Arrange(h => h.SendGetRequest(SetProcessingUrl))
                    .Returns("OK")
                .OccursOnce();

            var task = _sut.SetProcessing("listId");

            task.Wait();

            var actual = task.Result;
            _httpService.Assert();
            actual.ShouldBeTrue();
        }
    }
}

