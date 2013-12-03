using AudioSwitcher.AudioApi.System;
using Jurassic;
using Xunit;

namespace AudioSwitcher.Scripting.Tests
{
    public class EngineTests
    {
        [Fact]
        public void Engine_Create()
        {
            var engine = new ScriptEngine();
            Assert.NotNull(engine);
        }

        [Fact]
        public void Engine_AddLibrary_Core()
        {
            var engine = new ScriptEngine();
            var coreLib = engine.AddCoreLibrary();

            Assert.Equal(true, engine.HasGlobalValue(coreLib.Name));
        }

        [Fact]
        public void Engine_AddLibrary_AudioSwitcher()
        {
            var engine = new ScriptEngine();
            var asLib = engine.AddAudioSwitcherLibrary(new SystemAudioContext());

            Assert.Equal(true, engine.HasGlobalValue(asLib.Name));
        }

        [Fact]
        public void Engine_RemoveLibrary_Core()
        {
            var engine = new ScriptEngine();
            var coreLib = engine.AddCoreLibrary();
            engine.RemoveLibrary(coreLib);

            Assert.Equal(engine.GetGlobalValue(coreLib.Name), Undefined.Value);
        }

        [Fact]
        public void Engine_RemoveLibrary_AudioSwitcher()
        {
            var engine = new ScriptEngine();
            var asLib = engine.AddAudioSwitcherLibrary(new SystemAudioContext());
            engine.RemoveLibrary(asLib);

            Assert.Equal(engine.GetGlobalValue(asLib.Name), Undefined.Value);
        }

    }
}
