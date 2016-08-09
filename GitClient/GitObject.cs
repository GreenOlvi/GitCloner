using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitClient
{
    public class GitObject
    {
        public GitObject(IEnumerable<byte> bytes)
        {
            Bytes = bytes.ToArray();
            Data = Encoding.UTF8.GetString(Bytes);

            var typeString = Data.Substring(0, Data.IndexOf(" ", StringComparison.InvariantCulture));
            GitObjectType type;
            if (!GitObjectType.TryParse(typeString, true, out type))
            {
                throw new Exception("Unknown git object type '" + typeString + "'");
            }
            Type = type;
        }

        public byte[] Bytes { get; set; }
        public string Data { get; }
        public GitObjectType Type { get; private set; }
    }

    public enum GitObjectType
    {
        Unknown = 0,
        Blob,
        Commit,
        Tree,
    }
}
