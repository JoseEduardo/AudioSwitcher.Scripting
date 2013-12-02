using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;

namespace AudioSwitcher.Scripting
{
    public interface IJavaScriptLibrary
    {

        string Name { get; }

        int Version { get; }

        void Add(ScriptEngine engine);

        void Remove(ScriptEngine engine);

    }
}
