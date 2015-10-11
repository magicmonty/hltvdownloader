using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Pagansoft.Homeload.Core
{
    [TestFixture]
    public class LinkListTests
    {
        [Test]
        public void ShouldBeInitializedCorrectly()
        {
            var actual = new LinkList();
            actual.ShouldSatisfyAllConditions(
                () => actual.ShouldBeEmpty(),
                () => actual.Id.ShouldBe(string.Empty, "Id"),
                () => actual.HappyHourStart.ShouldBeNull("HappyHourStart"),
                () => actual.HappyHourEnd.ShouldBeNull("HappyHourEnd"),
                () => actual.Error.ShouldBeEmpty("Error"),
                () => actual.NumberOfLinks.ShouldBe(0, "NumberOfLinks"),
                () => actual.LinkCount.ShouldBe(0, "LinkCount"),
                () => actual.Interval.ShouldBe(0, "Interval"));
        }

        [Test]
        public void ShouldReturnEmptyListWithErrorOnParseIfValueContainsError()
        {
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("USER_NOT_FOUND");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("WRONG_PASSWORD");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("DB_ERROR");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("NOT_ALLOWED");
            ShouldReturnEmptyListWithErrorOnParseIfValueContainsError("NO_NEW_LINKS");
        }

        public void ShouldReturnEmptyListWithErrorOnParseIfValueContainsError(string response)
        {
            var actual = LinkList.Parse(response);

            actual.LinkCount.ShouldBe(0);
            actual.Error.ShouldBe(response);
        }

        [Test]
        public void ShouldParseCorrectly()
        {
            const string response = @"INTERVAL=60;NUMBER_OF_LINKS=1;LIST=3828;LINKCOUNT=2;HHSTART=0;HHEND=8;
http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi;11272262;
http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi;11274936;";

            var actual = LinkList.Parse(response);
            actual.Interval.ShouldBe(60, "Interval");
            actual.NumberOfLinks.ShouldBe(1, "NumberOfLinks");
            actual.Id.ShouldBe("3828", "Id");
            actual.HappyHourStart.ShouldBe(0, "HappyHourStart");
            actual.HappyHourEnd.ShouldBe(8, "HappyHourEnd");

            actual.LinkCount.ShouldBe(2, "LinkCount");

            var link1 = actual.First();
            var link2 = actual.Last();

            link1.Url.ShouldBe("http://81.95.11.6/download/1811986/1/7875449/805df047b6ed2dd2cfe1cea7a6da49ed/de/Unser_Sandmaennchen_13.08.29_18-54_mdr_6_TVOON_DE.mpg.avi", "Link1 URL");
            link1.Id.ShouldBe("11272262", "Link1 ID");
            link2.Url.ShouldBe("http://81.95.11.6/download/1811986/1/7878195/458cd9d2b67c8704ab731ea9853306a2/de/Unser_Sandmaennchen_13.08.30_18-54_mdr_6_TVOON_DE.mpg.avi", "Link2 URL");
            link2.Id.ShouldBe("11274936", "Link2 ID");
        }

        [Test]
        public void ShouldParseEmptyListCorrectly()
        {
            const string response = @"INTERVAL=60;NUMBER_OF_LINKS=0;LIST=0;LINKCOUNT=0;HHSTART=0;HHEND=8;";

            var actual = LinkList.Parse(response);
            actual.Interval.ShouldBe(60, "Interval");
            actual.NumberOfLinks.ShouldBe(0, "NumberOfLinks");
            actual.Id.ShouldBe("0", "Id");
            actual.HappyHourStart.ShouldBe(0, "HappyHourStart");
            actual.HappyHourEnd.ShouldBe(8, "HappyHourEnd");
            actual.LinkCount.ShouldBe(0, "LinkCount");
        }
    }
}

