using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi;
using Jurassic.Library;

namespace AudioSwitcher.Scripting.Libraries
{
    public sealed partial class AudioSwitcherLibrary
    {

        /// <summary>
        /// Macro function used to list all the devices
        /// </summary>
        /// <param name="dFlags">0 Both, 1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevices")]
        public ArrayInstance GetAudioDevices(int dFlags)
        {
            int flags = (int)dFlags;
            var devices = new List<AudioDevice>();
            switch (flags)
            {
                case 0:
                    devices.AddRange(Context.Controller.GetPlaybackDevices());
                    devices.AddRange(Context.Controller.GetRecordingDevices());
                    break;
                case 1:
                    devices.AddRange(Context.Controller.GetPlaybackDevices());
                    break;
                case 2:
                    devices.AddRange(Context.Controller.GetRecordingDevices());
                    break;
            }

            //Love LINQ <3
            return this.Engine.Array.New(devices.Select(x => new JavaScriptAudioDevice(this.Engine, this.Context, x)).ToArray<object>());
        }

        /// <summary>
        /// Macro function used to list all the devices
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevice")]
        public JavaScriptAudioDevice GetAudioDevice(string name)
        {

            AudioDevice dev = this.Context.Controller.GetPlaybackDevices()
                .Concat(this.Context.Controller.GetRecordingDevices())
                .FirstOrDefault(x => x.ShortName == name);

            return dev != null ? new JavaScriptAudioDevice(this.Engine, this.Context, dev) : null;

            //Love LINQ <3
            //return this.Engine.Array.New(devices.Select(x => new JavaScriptAudioDevice(this.Engine, this.Context, x)).ToArray<object>());
        }

    }
}
