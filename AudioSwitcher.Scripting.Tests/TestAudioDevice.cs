using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi;

namespace AudioSwitcher.Scripting.Tests
{
    public sealed class TestAudioDevice : AudioDevice
    {
        public TestAudioDevice(Guid id, DataFlow dFlow, IDeviceEnumerator enumerator)
            : base(enumerator)
        {
        }

        public Guid _id;
        public override Guid ID
        {
            get
            {
                return _id;
            }
        }

        public override string Description
        {
            get { return ID.ToString(); }
        }

        public override string ShortName
        {
            get { return ID.ToString(); }
        }

        public override string SystemName
        {
            get { return ID.ToString(); }
        }

        public override string FullName
        {
            get { return ID.ToString(); }
        }

        public override DeviceState State
        {
            get { return DeviceState.Active; }
        }

        private DataFlow _dataFlow;
        public override DataFlow DataFlow
        {
            get
            {
                return _dataFlow;
            }
        }

        private bool _muted = false;
        public override bool IsMuted
        {
            get
            {
                return _muted;
            }
        }

        public override int Volume
        {
            get;
            set;
        }

        public override bool Mute()
        {
            return _muted = true;
        }

        public override bool UnMute()
        {
            return _muted = false;
        }

    }
}
