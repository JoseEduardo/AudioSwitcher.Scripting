using AudioSwitcher.AudioApi.System;
using Jurassic;
using Xunit;

namespace AudioSwitcher.Scripting.Tests
{
    public class AudioSwitcherLibraryTests
    {

        [Fact]
        public void AudioSwitcher_getAudioDevices()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            Assert.DoesNotThrow(() => engine.Execute("AudioSwitcher.getAudioDevices(1)"));
        }

        [Fact]
        public void AudioSwitcher_getAudioDeviceByName()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            string js = @"AudioSwitcher.getAudioDevice(AudioSwitcher.getAudioDevices(1)[0].name);";

            Assert.DoesNotThrow(() => engine.Execute(js));
            var audioDevice = engine.Evaluate<JavaScriptAudioDevice>(js);
            Assert.NotEqual(null, audioDevice);
            Assert.IsType<JavaScriptAudioDevice>(audioDevice);
        }

        [Fact]
        public void AudioSwitcher_AudioDevice_Exists()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            string js = @"AudioSwitcher.getAudioDevices(1)[0];";

            Assert.DoesNotThrow(() => engine.Execute(js));
            Assert.NotEqual(null, engine.Evaluate<JavaScriptAudioDevice>(js));
            Assert.IsType<JavaScriptAudioDevice>(engine.Evaluate<JavaScriptAudioDevice>(js));
        }

        [Fact]
        public void AudioSwitcher_AudioDevice_toggleMute()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            string js = @"AudioSwitcher.getAudioDevices(1)[0].toggleMute()";

            //Toggles the mute and tests non equality of state
            var isMuted = engine.Evaluate<bool>(js);
            Assert.NotEqual(isMuted, engine.Evaluate<bool>(js));
        }

        [Fact]
        public void AudioSwitcher_AudioDevice_setMute_true()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            string js = @"AudioSwitcher.getAudioDevices(1)[0].setMute(true)";

            //Sets to muted
            Assert.Equal(true, engine.Evaluate<bool>(js));
        }

        [Fact]
        public void AudioSwitcher_AudioDevice_setMute_false()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            string js = @"AudioSwitcher.getAudioDevices(1)[0].setMute(false)";

            //unmutes
            Assert.Equal(false, engine.Evaluate<bool>(js));
        }

    }
}
