using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClient
{
    public class GitClient
    {
        public GitClient(IGitObjectProvider provider)
        {
            ObjectProvider = provider;
        }

        public IGitObjectProvider ObjectProvider { get; private set; }
    }
}
