using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.System;
using Jurassic;
using Xunit;

namespace AudioSwitcher.Scripting.Tests
{
    public class AudioSwitcherLibraryTests
    {

        public static AudioContext GetAudioContext()
        {
            return new SystemAudioContext();
        }

        [Fact]
        public void Engine_AddLibrary_AudioSwitcher()
        {
            var engine = new ScriptEngine();
            var asLib = engine.AddAudioSwitcherLibrary(GetAudioContext());

            Assert.Equal(true, engine.HasGlobalValue(asLib.Name));
        }

        [Fact]
        public void Engine_RemoveLibrary_AudioSwitcher()
        {
            var engine = new ScriptEngine();
            var asLib = engine.AddAudioSwitcherLibrary(GetAudioContext());
            engine.RemoveLibrary(asLib);

            Assert.Equal(engine.GetGlobalValue(asLib.Name), Undefined.Value);
        }


        [Fact]
        [Trait("Type", "AudioLibrary")]
        public void AudioSwitcher_getAudioDevices()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            Assert.DoesNotThrow(() => engine.Execute("AudioSwitcher.getAudioDevices(1)"));
        }

        [Fact]
        [Trait("Type", "AudioLibrary")]
        public void AudioSwitcher_getAudioDeviceByName()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevice(AudioSwitcher.getAudioDevices(1)[0].name);";

            Assert.DoesNotThrow(() => engine.Execute(js));
            var audioDevice = engine.Evaluate<JavaScriptAudioDevice>(js);
            Assert.NotEqual(null, audioDevice);
            Assert.IsType<JavaScriptAudioDevice>(audioDevice);
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        public void AudioSwitcher_AudioDevice_Exists()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0];";

            Assert.DoesNotThrow(() => engine.Execute(js));
            Assert.NotEqual(null, engine.Evaluate<JavaScriptAudioDevice>(js));
            Assert.IsType<JavaScriptAudioDevice>(engine.Evaluate<JavaScriptAudioDevice>(js));
        }

        [Fact]
        [Trait("Function", "Mute")]
        public void AudioSwitcher_AudioDevice_toggleMute()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].toggleMute()";

            //Toggles the mute and tests non equality of state
            var isMuted = engine.Evaluate<bool>(js);
            Assert.NotEqual(isMuted, engine.Evaluate<bool>(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "Mute")]
        public void AudioSwitcher_AudioDevice_setMute_true()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].setMute(true)";

            //Sets to muted
            Assert.Equal(true, engine.Evaluate<bool>(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "Mute")]
        public void AudioSwitcher_AudioDevice_setMute_false()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].setMute(false)";

            //unmutes
            Assert.Equal(false, engine.Evaluate<bool>(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "Volume")]
        public void AudioSwitcher_AudioDevice_getVolume()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].volume()";

            //don't care what it retuns, just that it exists
            Assert.DoesNotThrow(() => engine.Execute(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "Volume")]
        public void AudioSwitcher_AudioDevice_setVolume()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string setTo10 = @"AudioSwitcher.getAudioDevices(1)[0].volume(10)";
            const string getVolume = @"AudioSwitcher.getAudioDevices(1)[0].volume()";
            var orignalVol = engine.Evaluate<int>(getVolume);
            string setToOriginal = @"AudioSwitcher.getAudioDevices(1)[0].volume(" + orignalVol + ")";

            //unmutes
            Assert.Equal(10, engine.Evaluate<int>(setTo10));
            Assert.Equal(10, engine.Evaluate<int>(getVolume));
            Assert.Equal(orignalVol, engine.Evaluate<int>(setToOriginal));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "Name")]
        public void AudioSwitcher_AudioDevice_getName()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].name";

            //unmutes
            Assert.DoesNotThrow(() => engine.Execute(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "ID")]
        public void AudioSwitcher_AudioDevice_getID()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].id";

            //unmutes
            Assert.DoesNotThrow(() => engine.Execute(js));
        }

        [Fact]
        [Trait("Type", "AudioDevice")]
        [Trait("Function", "ID")]
        public void AudioSwitcher_AudioDevice_getFlags()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(GetAudioContext());

            const string js = @"AudioSwitcher.getAudioDevices(1)[0].flags";

            //unmutes
            Assert.DoesNotThrow(() => engine.Execute(js));
        }

    }
}
