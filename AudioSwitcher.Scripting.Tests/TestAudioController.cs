using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi;

namespace AudioSwitcher.Scripting.Tests
{
    public sealed class TestAudioController : AudioController
    {
        public TestAudioController(IDeviceEnumerator enumerator)
            : base(enumerator)
        {
        }
    }
}
