using AudioSwitcher.AudioApi;

namespace AudioSwitcher.Scripting.Debug
{
    public class DebugAudioContext : AudioContext
    {

        public DebugAudioContext()
            : this(null)
        {
        }

        public DebugAudioContext(PreferredDeviceManager preferredDeviceManager)
            : base(new DebugAudioController(), preferredDeviceManager)
        {
        }

        public DebugAudioContext(AudioController<AudioDevice> controller, PreferredDeviceManager preferredDeviceManager)
            : base(controller, preferredDeviceManager)
        {
        }

    }
}
