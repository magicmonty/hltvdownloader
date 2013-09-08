using NUnit.Framework;
using Moq;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class UrlBuilderTest
    {
        private Mock<IConfiguration> _configuration;
        private UrlBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c.HltvUserName).Returns("user");
            _configuration.Setup(c => c.HltvPassword).Returns("password");

            _sut = new UrlBuilder(_configuration.Object);
        }

        [Test]
        public void ShouldBuildCorrectGetLinksUrl()
        {
            Assert.That(
                _sut.BuildGetLinksUrl(), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99"));
        }

        [Test]
        public void ShouldBuildCorrectGetLinksUrlWithProcessingToNew()
        {
            Assert.That(
                _sut.BuildGetLinksUrl(processingToNew: true), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true"));
        }

        [Test]
        public void ShouldBuildCorrectSetProcessingUrl()
        {
            Assert.That(
                _sut.BuildSetProcessingUrl("12345"), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=12345&state=processing"));
        }

        [Test]
        public void ShouldBuildCorrectSetStateUrl()
        {
            Assert.That(
                _sut.BuildSetStateUrl("54321", "finished"), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=finished&error="));
        }

        [Test]
        public void ShouldBuildCorrectSetErrorUrl()
        {
            Assert.That(
                _sut.BuildSetErrorUrl("54321"), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&state=finished&error=brokenonopen"));
        }
    }
}

