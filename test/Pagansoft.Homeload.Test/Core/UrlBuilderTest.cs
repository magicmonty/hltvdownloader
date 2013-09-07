using NUnit.Framework;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class UrlBuilderTest
    {
        [Test]
        public void ShouldBuildCorrectGetLinksUrl()
        {
            var _sut = new UrlBuilder("user", "password");

            Assert.That(
                _sut.BuildGetLinksUrl(), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99"));
        }

        [Test]
        public void ShouldBuildCorrectGetLinksUrlWithProcessingToNew()
        {
            var _sut = new UrlBuilder("user", "password");

            Assert.That(
                _sut.BuildGetLinksUrl(processingToNew: true), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=getlinks&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&proctonew=true"));
        }

        [Test]
        public void ShouldBuildCorrectSetProcessingUrl()
        {
            var _sut = new UrlBuilder("user", "password");

            Assert.That(
                _sut.BuildSetProcessingUrl("12345"), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&list=12345&state=processing"));
        }

        [Test]
        public void ShouldBuildCorrectSetStateUrl()
        {
            var _sut = new UrlBuilder("user", "password");

            Assert.That(
                _sut.BuildSetStateUrl("54321", "12345", "finished"), 
                Is.EqualTo("http://www.homeloadtv.com/api/?do=setstate&uid=user&password=5F4DCC3B5AA765D61D8327DEB882CF99&id=54321&list=12345&state=finished"));
        }
    }
}

