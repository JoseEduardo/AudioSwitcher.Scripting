using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi.System;
using Jurassic;
using Xunit;

namespace AudioSwitcher.Scripting.Tests
{
    public class AudioSwitcherLibraryTests
    {

        [Fact]
        public void AudioSwitcher_getAudioDevices_Exists()
        {
            var engine = new ScriptEngine();
            engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            Assert.DoesNotThrow(() => engine.Execute("AudioSwitcher.getAudioDevices(1)"));
        }

    }
}
