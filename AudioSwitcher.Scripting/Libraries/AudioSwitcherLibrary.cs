using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.System;
using Jurassic;
using Jurassic.Library;

namespace AudioSwitcher.Scripting.Libraries
{
    public sealed partial class AudioSwitcherLibrary : ObjectInstance, IJavaScriptLibrary
    {

        public string Name
        {
            get
            {
                return "AudioSwitcher";
            }
        }

        public int Version
        {
            get
            {
                return 1;
            }
        }

        public AudioContext Context
        {
            get;
            private set;
        }

        public AudioSwitcherLibrary(ScriptEngine engine, AudioContext context)
            : base(engine)
        {
            Context = context;
            this.PopulateFields();
            this.PopulateFunctions();
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
