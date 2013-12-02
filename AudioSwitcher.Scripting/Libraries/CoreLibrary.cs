using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AudioSwitcher.AudioApi;
using Jurassic;
using Jurassic.Library;

namespace AudioSwitcher.Scripting.Libraries
{
    public sealed partial class CoreLibrary : ObjectInstance, IJavaScriptLibrary
    {
        public CoreLibrary(ScriptEngine engine)
            : base(engine)
        {
            this.PopulateFields();
            this.PopulateFunctions();
        }

        public string Name
        {
            get
            {
                return "Core";
            }
        }

        public int Version
        {
            get
            {
                return 1;
            }
        }

        public void Add(ScriptEngine engine)
        {
            if (engine.GetGlobalValue(Name) == Undefined.Value)
                engine.SetGlobalValue(Name, this);
        }

        public void Remove(ScriptEngine engine)
        {
            if (engine.GetGlobalValue(Name) != Undefined.Value)
                engine.Global.Delete(Name, false);
        }

    }
}
