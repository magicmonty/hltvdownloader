using NUnit.Framework;
using Shouldly;
using Moq;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class UrlBuilderTests
    {
        IConfiguration _configuration;
        UrlBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Mock.Of<IConfiguration>(c => 
                c.HltvUserName == "user" &&
                c.HltvPassword == "password");

            _sut = new UrlBuilder(_configuration);
        }

        [Test]
        public void ShouldBuildCorrectGetLinksUrl()
        {
            _sut
                .BuildGetLinksUrl()
                .ShouldBe("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99");
        }

        [Test]
        public void ShouldBuildCorrectGetLinksUrlWithProcessingToNew()
        {
            _sut
                .BuildGetLinksUrl(true)
                .ShouldBe("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true");
        }

        [Test]
        public void ShouldBuildCorrectSetProcessingUrl()
        {
            _sut
                .BuildSetProcessingUrl("12345")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=12345&state=processing");
        }

        [Test]
        public void ShouldBuildCorrectSetStateUrl()
        {
            _sut
                .BuildSetStateUrl("54321", "finished")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=finished&error=");
        }

        [Test]
        public void ShouldBuildCorrectSetErrorUrl()
        {
            _sut
                .BuildSetErrorUrl("54321")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=error&error=brokenonopen");
        }
    }
}

