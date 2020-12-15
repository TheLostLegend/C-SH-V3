using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigManager
{
    public interface IBase
    {
        public StrEnter GetOptions(string configPath);
    }
}
