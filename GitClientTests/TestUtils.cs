using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace GitClientTests
{
    public static class TestUtils
    {
        const string REPOS_DIR = "repos";

        public static void SetUpRepo(string repoName)
        {
            var dir = RepoDirPath(repoName);
            TearDownRepo(dir);

            using (var zip = ZipFile.Read(Path.Combine(REPOS_DIR, repoName + ".zip")))
            {
                zip.ExtractAll(REPOS_DIR);
            }
        }

        public static void TearDownRepo(string repoName)
        {
            var dir = RepoDirPath(repoName);
            if (Directory.Exists(dir))
            {
                var directory = new DirectoryInfo(dir) {Attributes = FileAttributes.Normal};
                foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    info.Attributes = FileAttributes.Normal;
                }
                directory.Delete(true);
            }
        }

        private static string RepoDirPath(string repoName)
        {
            return Path.Combine(REPOS_DIR, repoName);
        }
    }
}
