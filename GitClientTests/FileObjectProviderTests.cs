using System.Linq;
using GitClient;
using NUnit.Framework;

namespace GitClientTests
{
    [TestFixture]
    public class FileObjectProviderTests
    {
        [SetUp]
        public void SetUp()
        {
            TestUtils.SetUpRepo("SimpleRepo");
        }

        [TearDown]
        public void TearDown()
        {
            TestUtils.TearDownRepo("SimpleRepo");
        }

        [Test]
        public void GetObjectStreamTest()
        {
            var provider = new FileObjectProvider(@"repos\SimpleRepo\.git");

            var c = provider.GetObject("15de639e7fdfad54e6c743d7a0a161a5731b6d9b");
            Assert.AreEqual(GitObjectType.Commit, c.Type);

            var t = provider.GetObject("57a512e77973717af1bc4bc65a0c338d33092600");
            Assert.AreEqual(GitObjectType.Tree, t.Type);
        }
    }
}
