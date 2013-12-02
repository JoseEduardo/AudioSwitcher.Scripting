using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Xunit;

namespace AudioSwitcher.Scripting.Tests
{
    public class CoreLibraryTests
    {

        [Fact]
        public void Core_sleep_Exists()
        {
            var engine = new ScriptEngine();
            engine.AddCoreLibrary();
            Assert.DoesNotThrow(() => engine.Execute("Core.sleep(10)"));
        }

    }
}
