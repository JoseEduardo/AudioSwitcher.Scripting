using System;
using AudioSwitcher.AudioApi;
using Jurassic;
using Jurassic.Library;


namespace AudioSwitcher.Scripting
{
    public sealed partial class JavaScriptAudioDevice : ObjectInstance
    {
        private string _id;
        [JSProperty(Name = "id")]
        public string ID
        {
            get
            {
                return _id;
            }
            internal set
            {
                _id = value;
            }
        }

        private string _name;
        [JSProperty(Name = "name")]
        public string Name
        {
            get
            {
                return _name;
            }
            internal set
            {
                _name = value;
            }
        }

        private int _flags;
        [JSProperty(Name = "flags")]
        public int Flags
        {
            get
            {
                return _flags;
            }
            internal set
            {
                _flags = value;
            }
        }

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
                return Context.Controller.GetAudioDevice(new Guid(this.ID));
            }
        }

        internal JavaScriptAudioDevice(ScriptEngine engine, AudioContext context, AudioDevice device)
            : base(engine)
        {
            Context = context;
            ID = device.ID.ToString();
            Name = device.ShortName;
            Flags = device.IsPlaybackDevice ? 1 : 2;

            this.PopulateFields();
            this.PopulateFunctions();
        }
    }
}
