using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace GitClient
{
    public class FileObjectProvider : IGitObjectProvider
    {
        public FileObjectProvider(string repositoryPath)
        {
            RepositoryPath = repositoryPath;
            var configPath = Path.Combine(RepositoryPath, "HEAD");
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Not a valid git repository path");
            }
        }

        public string RepositoryPath { get; }

        public Stream GetObjectStream(string id)
        {
            var path = GetObjectPath(id);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Object not found", path);
            }

            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new ZlibStream(fileStream, CompressionMode.Decompress);
        }

        public byte[] GetObjectBytes(string id)
        {
            var stream = GetObjectStream(id);
            byte[] buffer = new byte[1024];
            var read = stream.Read(buffer, 0, 1024);
            stream.Close();

            return buffer.Take(read).ToArray();
        }

        public GitObject GetObject(string id)
        {
            return new GitObject(GetObjectBytes(id));
        }

        public T GetObject<T>(string id) where T : GitObject, new()
        {
            return new T() { Bytes = GetObjectBytes(id) };
        }

        private string GetObjectPath(string id)
        {
            var prefix = id.Substring(0, 2);
            var name = id.Substring(2);
            return Path.Combine(RepositoryPath, "objects", prefix, name);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
