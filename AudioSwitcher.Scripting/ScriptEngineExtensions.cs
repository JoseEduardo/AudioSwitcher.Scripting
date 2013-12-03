using AudioSwitcher.AudioApi;
using AudioSwitcher.Scripting.Libraries;
using Jurassic;

namespace AudioSwitcher.Scripting
{
    public static class ScriptEngineExtensions
    {
        public static AudioSwitcherLibrary AddAudioSwitcherLibrary(this ScriptEngine engine, AudioContext context)
        {
            return AddLibrary(engine, new AudioSwitcherLibrary(engine, context));
        }

        public static CoreLibrary AddCoreLibrary(this ScriptEngine engine)
        {
            return AddLibrary(engine, new CoreLibrary(engine));
        }

        /// <summary>
        ///     Add the library to the current lua context
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="library"></param>
        /// <returns></returns>
        public static bool AddLibrary(this ScriptEngine engine, IJavaScriptLibrary library)
        {
            library.Add(engine);
            return true;
        }

        /// <summary>
        ///     Add the library to the current lua context
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="library"></param>
        /// <returns></returns>
        public static T AddLibrary<T>(this ScriptEngine engine, T library) where T : IJavaScriptLibrary
        {
            library.Add(engine);
            return library;
        }

        /// <summary>
        ///     Removes the library import from the current lua context
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="library"></param>
        /// <returns></returns>
        public static bool RemoveLibrary(this ScriptEngine engine, IJavaScriptLibrary library)
        {
            library.Remove(engine);
            return true;
        }
    }
}