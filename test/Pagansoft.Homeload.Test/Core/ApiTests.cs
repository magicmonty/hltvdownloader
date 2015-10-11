using System.Linq;
using NUnit.Framework;
using Shouldly;
using Moq;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
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
        Mock<IHLTCHttpService> _httpService;
        Mock<IConfiguration> _configuration;

        private void CreateSut()
        {
            _httpService = new Mock<IHLTCHttpService>();
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c.HltvUserName).Returns("user");
            _configuration.Setup(c => c.HltvPassword).Returns("password");

            _sut = new Api(_httpService.Object, new UrlBuilder(_configuration.Object));
        }

        [Test]
        public async void ShouldReturnLinkListOnSuccessfulGetInitialLinks()
        {
            CreateSut();
            _httpService
                .Setup(h => h.SendGetRequest(GetLinksUrlProcToNew))
                .ReturnsAsync(LinkListResponse);

            var actual = await _sut.GetLinks(initial: true);

            _httpService.Verify();

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

        [Test]
        public async void ShouldReturnLinkListOnSuccessfulGetLinks()
        {
            CreateSut();

            _httpService
                .Setup(h => h.SendGetRequest(GetLinksUrl))
                .ReturnsAsync(LinkListResponse);

            var actual = await _sut.GetLinks();

            _httpService.Verify();

            VerifyLinkList(actual);
        }

        [Test]
        public async void ShouldExecuteSetStateInHttpServiceIfSetStateIsTriggered()
        {
            CreateSut();

            _httpService
                .Setup(h => h.SendGetRequest(SetStateUrl))
                .ReturnsAsync("OK");

            var actual = await _sut.SetState("linkId", LinkState.Finished);
           
            _httpService.Verify();
           
            actual.ShouldBeTrue();
        }

        [Test]
        public async void ShouldExecuteSetErrorInHttpServiceIfSetErrorIsTriggered()
        {
            CreateSut();

            _httpService
                .Setup(h => h.SendGetRequest(SetErrorUrl))
                .ReturnsAsync("OK");

            var actual = await _sut.SetError("linkId");

            _httpService.Verify();

            actual.ShouldBeTrue();
        }

        [Test]
        public async void ShouldExecuteSetProcessingInHttpServiceIfSetProcessingIsTriggered()
        {
            CreateSut();

            _httpService
                .Setup(h => h.SendGetRequest(SetProcessingUrl))
                .ReturnsAsync("OK");

            var actual = await _sut.SetProcessing("listId");

            _httpService.Verify();

            actual.ShouldBeTrue();
        }
    }
}

