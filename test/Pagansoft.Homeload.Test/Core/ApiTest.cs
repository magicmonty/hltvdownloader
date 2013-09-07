using NUnit.Framework;
using Moq;
using System.Linq;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class ApiTest
    {
        private const string LinkListResponse = @"INTERVAL=60;NUMBER_OF_LINKS=1;LIST=3828;LINKCOUNT=2;HHSTART=0;HHEND=8;
http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi;11272262;
http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi;11274936;";
        private const string GetLinksUrl = "http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99";
        private const string GetLinksUrlProcToNew = "http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true";
        private const string SetStateUrl = "http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=linkId&list=listId&state=finished";
        private const string SetProcessingUrl = "http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=listId&state=processing";
        private Api _sut;
        private Mock<IHLTCHttpService> _httpService;

        [SetUp]
        public void SetUp()
        {
            _httpService = new Mock<IHLTCHttpService>();
            _sut = new Api(_httpService.Object, new UrlBuilder("user", "password"));
        }

        [Test]
        public void ShouldReturnLinkListOnSuccessfulGetInitialLinks()
        {
            _httpService.Setup(h => h.SendGetRequest(GetLinksUrlProcToNew))
                        .Returns(LinkListResponse)
                        .Verifiable();

            var task = _sut.GetLinks(initial: true);
            task.Wait();
            var actual = task.Result;

            _httpService.Verify();

            VerifyLinkList(actual);
        }

        private static void VerifyLinkList(LinkList actual)
        {
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
        public void ShouldReturnLinkListOnSuccessfulGetLinks()
        {
            _httpService.Setup(h => h.SendGetRequest(GetLinksUrl))
                        .Returns(LinkListResponse)
                        .Verifiable();

            var task = _sut.GetLinks();
            task.Wait();
            var actual = task.Result;

            _httpService.Verify();

            VerifyLinkList(actual);
        }

        [Test]
        public void ShouldExecuteSetStateInHttpServiceIfSetStateIsTriggered()
        {
            _httpService.Setup(h => h.SendGetRequest(SetStateUrl))
                        .Returns("OK")
                        .Verifiable();

            var task = _sut.SetState("linkId", "listId", LinkState.Finished);

            task.Wait();

            var actual = task.Result;
            _httpService.Verify();
            Assert.That(actual, Is.True);
        }

        [Test]
        public void ShouldExecuteSetProcessingInHttpServiceIfSetProcessingIsTriggered()
        {
            _httpService.Setup(h => h.SendGetRequest(SetProcessingUrl))
                    .Returns("OK")
                    .Verifiable();

            var task = _sut.SetProcessing("listId");

            task.Wait();

            var actual = task.Result;
            _httpService.Verify();
            Assert.That(actual, Is.True);
        }
    }
}

