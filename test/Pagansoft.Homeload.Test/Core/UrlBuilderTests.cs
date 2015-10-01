using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    public class UrlBuilderTests
    {
        IConfiguration _configuration;
        UrlBuilder _sut;

        public void SetUp()
        {
            _configuration = Mock.Create<IConfiguration>();
            _configuration.Arrange(c => c.HltvUserName).Returns("user");
            _configuration.Arrange(c => c.HltvPassword).Returns("password");

            _sut = new UrlBuilder(_configuration);
        }

        [Fact]
        public void ShouldBuildCorrectGetLinksUrl()
        {
            SetUp();

            _sut
                .BuildGetLinksUrl()
                .ShouldBe("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99");
        }

        [Fact]
        public void ShouldBuildCorrectGetLinksUrlWithProcessingToNew()
        {
            SetUp();
            _sut
                .BuildGetLinksUrl(true)
                .ShouldBe("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true");
        }

        [Fact]
        public void ShouldBuildCorrectSetProcessingUrl()
        {
            SetUp();
            _sut
                .BuildSetProcessingUrl("12345")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=12345&state=processing");
        }

        [Fact]
        public void ShouldBuildCorrectSetStateUrl()
        {
            SetUp();
            _sut
                .BuildSetStateUrl("54321", "finished")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=finished&error=");
        }

        [Fact]
        public void ShouldBuildCorrectSetErrorUrl()
        {
            SetUp();
            _sut
                .BuildSetErrorUrl("54321")
                .ShouldBe("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=finished&error=brokenonopen");
        }
    }
}

