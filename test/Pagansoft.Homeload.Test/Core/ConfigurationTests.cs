using System.Configuration;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    public class ConfigurationTests
    {
        [Fact]
        public void ShouldReturnCorrectUserName()
        {
            var appsettings = new KeyValueConfigurationCollection();
            appsettings.Add("username", "user");
            appsettings.Add("password", "pass");

            var configurationManager = Mock.Create<IConfigurationManager>();
            configurationManager.Arrange(c => c.AppSettings)
                .Returns(appsettings);

            var _sut = new Configuration(configurationManager);
            _sut.ShouldSatisfyAllConditions(
                () => _sut.HltvUserName.ShouldBe("user"),
                () => _sut.HltvPassword.ShouldBe("pass"));
        }
    }
}

