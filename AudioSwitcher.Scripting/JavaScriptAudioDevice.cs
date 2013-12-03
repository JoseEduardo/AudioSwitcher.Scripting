using System;
using AudioSwitcher.AudioApi;
using Jurassic;
using Jurassic.Library;


namespace AudioSwitcher.Scripting
{
    public sealed partial class JavaScriptAudioDevice : ObjectInstance
    {
        [JSProperty(Name = "id")]
        public string ID { get; internal set; }

        [JSProperty(Name = "name")]
        public string Name { get; internal set; }

        [JSProperty(Name = "flags")]
        public int Flags { get; internal set; }

        AudioContext Context
        {
            get;
            set;

        }

        AudioDevice Device
        {
            get
            {
                //Ensures that the Controller is always referencing the correct device
                //instance
                return Context.Controller.GetAudioDevice(new Guid(ID));
            }
        }

        internal JavaScriptAudioDevice(ScriptEngine engine, AudioContext context, AudioDevice device)
            : base(engine)
        {
            Context = context;
            ID = device.ID.ToString();
            Name = device.ShortName;
            Flags = device.IsPlaybackDevice ? 1 : 2;

            PopulateFields();
            PopulateFunctions();
        }
    }
}
