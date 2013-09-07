using System.Collections.Specialized;
using Moq;
using NUnit.Framework;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class ConfigurationTest
    {
        private Configuration _sut;

        [SetUp]
        public void SetUp()
        {
            var appsettings = new NameValueCollection();
            appsettings.Set("username", "user");
            appsettings.Set("password", "pass");

            var configurationManager = new Mock<IConfigurationManager>();
            configurationManager.Setup(c => c.AppSettings)
                .Returns(appsettings);

            _sut = new Configuration(configurationManager.Object);
        }

        [Test]
        public void ShouldReturnCorrectUserName()
        {
            Assert.That(_sut.HltvUserName, Is.EqualTo("user"));
            Assert.That(_sut.HltvPassword, Is.EqualTo("pass"));
        }
    }
}

