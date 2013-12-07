using AudioSwitcher.AudioApi;

namespace AudioSwitcher.Scripting.Debug
{
    public class DebugAudioController : AudioController<DebugAudioDevice>
    {
        public DebugAudioController()
            : base(new DebugSystemDeviceEnumerator())
        {
        }
    }
}
