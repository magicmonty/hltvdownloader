using System.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class ConfigurationTest
    {
        Configuration _sut;

        [SetUp]
        public void SetUp()
        {
            var appsettings = new KeyValueConfigurationCollection();
            appsettings.Add("username", "user");
            appsettings.Add("password", "pass");

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

