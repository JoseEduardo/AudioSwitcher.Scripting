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
        /// <param name="flags">0 Both, 1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevices")]
        public ArrayInstance GetAudioDevices([DefaultParameterValue(0)]int flags = 0)
        {
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
        /// <param name="flags"></param>
        /// <returns></returns>
        [JSFunction(Name = "getAudioDevice")]
        public JavaScriptAudioDevice GetAudioDevice(string name, [DefaultParameterValue(0)]int flags = 0)
        {
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

            var dev = devices.FirstOrDefault(x => x.ShortName == name);

            return dev != null ? new JavaScriptAudioDevice(Engine, Context, dev) : null;
        }

        /// <summary>
        ///     Gets all preferred devices
        /// </summary>
        /// <param name="flags">0 Both, 1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "getPreferredDevices")]
        public ArrayInstance GetPreferredDevices(int flags)
        {
            var devices = new List<AudioDevice>();

            switch (flags)
            {
                case 0:
                    devices.AddRange(Context.PreferredDeviceManager.PreferredDevices);
                    break;
                case 1:
                    devices.AddRange(Context.PreferredDeviceManager.PreferredDevices.Where(x => x.IsPlaybackDevice));
                    break;
                case 2:
                    devices.AddRange(Context.PreferredDeviceManager.PreferredDevices.Where(x => x.IsRecordingDevice));
                    break;
            }

            return Engine.EnumerableToArray(devices);
        }

        /// <summary>
        ///     Gets the next 
        /// </summary>
        /// <param name="flags">1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "nextPreferredDevice")]
        public JavaScriptAudioDevice NextPreferredDevice(int flags)
        {
            if (Context.PreferredDeviceManager == null)
                return null;

            switch (flags)
            {
                case 1:
                    return new JavaScriptAudioDevice(Engine, Context,
                        Context.PreferredDeviceManager.NextPlaybackDevice());
                case 2:
                    return new JavaScriptAudioDevice(Engine, Context,
                        Context.PreferredDeviceManager.NextRecordingDevice());
            }

            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="flags">1 = Playback, 2 = Recording</param>
        /// <returns></returns>
        [JSFunction(Name = "previousPreferredDevice")]
        public JavaScriptAudioDevice PreviousPreferredPlaybackDevice(int flags)
        {
            if (Context.PreferredDeviceManager == null)
                return null;

            switch (flags)
            {
                case 1:
                    return new JavaScriptAudioDevice(Engine, Context,
                        Context.PreferredDeviceManager.NextPlaybackDevice());
                case 2:
                    return new JavaScriptAudioDevice(Engine, Context,
                        Context.PreferredDeviceManager.NextRecordingDevice());
            }

            return null;
        }

    }
}