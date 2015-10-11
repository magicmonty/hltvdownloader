using System.Configuration;
using NUnit.Framework;
using Shouldly;
using Moq;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ShouldReturnCorrectUserName()
        {
            var appsettings = new KeyValueConfigurationCollection();
            appsettings.Add("username", "user");
            appsettings.Add("password", "pass");

            var configurationManager =  new Mock<IConfigurationManager>();
            configurationManager.Setup(c => c.AppSettings).Returns(appsettings);

            var _sut = new Configuration(configurationManager.Object);
            _sut.ShouldSatisfyAllConditions(
                () => _sut.HltvUserName.ShouldBe("user"),
                () => _sut.HltvPassword.ShouldBe("pass"));
        }
    }
}

