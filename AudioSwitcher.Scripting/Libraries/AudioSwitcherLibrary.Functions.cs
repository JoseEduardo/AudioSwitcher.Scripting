using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi;
using Jurassic.Library;

namespace AudioSwitcher.Scripting.Libraries
{
    public sealed partial class AudioSwitcherLibrary
    {
        /// <summary>
        ///     Macro function used to list all the devices
        /// </summary>
        /// <param name="dFlags">0 Both, 1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevices")]
        public ArrayInstance GetAudioDevices(int dFlags)
        {
            int flags = dFlags;
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
            return Engine.Array.New(devices.Select(x => new JavaScriptAudioDevice(Engine, Context, x)).ToArray<object>());
        }

        /// <summary>
        ///     Macro function used to list all the devices
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevice")]
        public JavaScriptAudioDevice GetAudioDevice(string name)
        {
            AudioDevice dev = Context.Controller.GetPlaybackDevices()
                .Concat(Context.Controller.GetRecordingDevices())
                .FirstOrDefault(x => x.ShortName == name);

            return dev != null ? new JavaScriptAudioDevice(Engine, Context, dev) : null;
        }

        /// <summary>
        ///     Gets all preferred devices
        /// </summary>
        /// <returns></returns>
        [JSFunction(Name = "getPreferredDevices")]
        public ArrayInstance GetPreferredDevices()
        {
            return
                Engine.EnumerableToArray(
                    Context.PreferredDeviceManager.PreferredDevices.Select(
                        x => new JavaScriptAudioDevice(Engine, Context, x)).ToArray<object>());
        }

        /// <summary>
        ///     Gets the next 
        /// </summary>
        /// <returns></returns>
        [JSFunction(Name = "nextPreferredPlaybackDevice")]
        public JavaScriptAudioDevice NextPreferredPlaybackDevice()
        {
            return Context.PreferredDeviceManager != null
                ? new JavaScriptAudioDevice(Engine, Context, Context.PreferredDeviceManager.NextPlaybackDevice())
                : null;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JSFunction(Name = "previousPreferredPlaybackDevice")]
        public JavaScriptAudioDevice PreviousPreferredPlaybackDevice()
        {
            return Context.PreferredDeviceManager != null
                ? new JavaScriptAudioDevice(Engine, Context, Context.PreferredDeviceManager.PreviousPlaybackDevice())
                : null;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JSFunction(Name = "nextPreferredRecordingDevice")]
        public JavaScriptAudioDevice NextPreferredRecordingDevice()
        {
            return Context.PreferredDeviceManager != null
                ? new JavaScriptAudioDevice(Engine, Context, Context.PreferredDeviceManager.NextRecordingDevice())
                : null;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JSFunction(Name = "previousPreferredPlaybackDevice")]
        public ObjectInstance PreviousPreferredRecordingDevice()
        {
            return Context.PreferredDeviceManager != null
                ? new JavaScriptAudioDevice(Engine, Context, Context.PreferredDeviceManager.PreviousRecordingDevice())
                : null;
        }
    }
}