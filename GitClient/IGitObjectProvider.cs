using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClient
{
    public interface IGitObjectProvider : IDisposable
    {
        Stream GetObjectStream(string id);
        byte[] GetObjectBytes(string id);
        GitObject GetObject(string id);
        //T GetObject<T>(string id) where T : GitObject;
    }
}
