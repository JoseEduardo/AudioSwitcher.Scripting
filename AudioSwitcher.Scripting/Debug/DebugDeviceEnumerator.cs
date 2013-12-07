using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.System;

namespace AudioSwitcher.Scripting.Debug
{
    public sealed class DebugSystemDeviceEnumerator : IDeviceEnumerator<DebugAudioDevice>
    {

        private readonly ConcurrentBag<DebugAudioDevice> _devices;
        private Guid _defaultPlaybackDeviceID;
        private Guid _defaultPlaybackCommDeviceID;
        private Guid _defaultRecordingDeviceID;
        private Guid _defaultRecordingCommDeviceID;


        public DebugSystemDeviceEnumerator()
        {
            _devices = new ConcurrentBag<DebugAudioDevice>();

            //Get a copy of the current system audio devices
            //then create a copy of the current state of the system
            //this allows us to "debug" macros against a "test" system
            var devEnum = new SystemDeviceEnumerator();
            _defaultPlaybackDeviceID = devEnum.DefaultPlaybackDevice.ID;
            _defaultPlaybackCommDeviceID = devEnum.DefaultCommunicationsPlaybackDevice.ID;
            _defaultRecordingDeviceID = devEnum.DefaultRecordingDevice.ID;
            _defaultRecordingCommDeviceID = devEnum.DefaultCommunicationsRecordingDevice.ID;

            foreach (var sysDev in devEnum.GetAudioDevices(DataFlow.All, DeviceState.Active | DeviceState.Unplugged | DeviceState.Disabled))
            {
                var dev = new DebugAudioDevice(this);
                dev.id = sysDev.ID;
                dev.description = sysDev.Description;
                dev.shortName = sysDev.ShortName;
                dev.systemName = sysDev.SystemName;
                dev.fullName = sysDev.FullName;
                dev.dataFlow = sysDev.DataFlow;
                dev.state = sysDev.State;
                try
                {
                    dev.Volume = sysDev.Volume;
                }
                catch
                {
                    dev.Volume = 0;
                }
                _devices.Add(dev);
            }
        }


        public DebugAudioDevice DefaultPlaybackDevice
        {
            get { return _devices.FirstOrDefault(x => x.ID == _defaultPlaybackDeviceID); }
        }

        public DebugAudioDevice DefaultCommunicationsPlaybackDevice
        {
            get { return _devices.FirstOrDefault(x => x.ID == _defaultPlaybackCommDeviceID); }
        }

        public DebugAudioDevice DefaultRecordingDevice
        {
            get { return _devices.FirstOrDefault(x => x.ID == _defaultRecordingDeviceID); }
        }

        public DebugAudioDevice DefaultCommunicationsRecordingDevice
        {
            get { return _devices.FirstOrDefault(x => x.ID == _defaultRecordingCommDeviceID); }
        }

        public DebugAudioDevice GetAudioDevice(Guid id)
        {
            return _devices.FirstOrDefault(x => x.ID == id);
        }

        public DebugAudioDevice GetDefaultAudioDevice(DataFlow dataflow, Role eRole)
        {
            switch (dataflow)
            {
                case DataFlow.Capture:
                    if (eRole == Role.Console || eRole == Role.Multimedia)
                        return DefaultRecordingDevice;

                    return DefaultCommunicationsRecordingDevice;
                case DataFlow.Render:
                    if (eRole == Role.Console || eRole == Role.Multimedia)
                        return DefaultPlaybackDevice;

                    return DefaultCommunicationsPlaybackDevice;
            }

            return null;
        }

        public IEnumerable<DebugAudioDevice> GetAudioDevices(DataFlow dataflow, DeviceState eRole)
        {
            return _devices.Where(x => 
                (x.dataFlow == dataflow || dataflow == DataFlow.All)
                && (x.State & eRole) > 0
                );
        }

        public bool SetDefaultDevice(DebugAudioDevice dev)
        {
            if (dev.IsPlaybackDevice)
            {
                _defaultPlaybackDeviceID = dev.ID;
                return true;
            }
            else if (dev.IsRecordingDevice)
            {
                _defaultRecordingDeviceID = dev.ID;
                return true;
            }

            return false;
        }

        public bool SetDefaultCommunicationsDevice(DebugAudioDevice dev)
        {
            if (dev.IsPlaybackDevice)
            {
                _defaultPlaybackCommDeviceID = dev.ID;
                return true;
            }
            else if (dev.IsRecordingDevice)
            {
                _defaultRecordingCommDeviceID = dev.ID;
                return true;
            }

            return false;
        }

        AudioDevice IDeviceEnumerator.DefaultPlaybackDevice
        {
            get { return this.DefaultPlaybackDevice; }
        }

        AudioDevice IDeviceEnumerator.DefaultCommunicationsPlaybackDevice
        {
            get { return this.DefaultCommunicationsPlaybackDevice; }
        }

        AudioDevice IDeviceEnumerator.DefaultRecordingDevice
        {
            get { return this.DefaultRecordingDevice; }
        }

        AudioDevice IDeviceEnumerator.DefaultCommunicationsRecordingDevice
        {
            get { return this.DefaultCommunicationsRecordingDevice; }
        }

        public event AudioDeviceChangedHandler AudioDeviceChanged;

        AudioDevice IDeviceEnumerator.GetAudioDevice(Guid id)
        {
            return this.GetAudioDevice(id);
        }

        AudioDevice IDeviceEnumerator.GetDefaultAudioDevice(DataFlow dataflow, Role eRole)
        {
            return this.GetDefaultAudioDevice(dataflow, eRole);
        }

        IEnumerable<AudioDevice> IDeviceEnumerator.GetAudioDevices(DataFlow dataflow, DeviceState state)
        {
            return this.GetAudioDevices(dataflow, state);
        }

        public bool SetDefaultDevice(AudioDevice dev)
        {
            var device = dev as DebugAudioDevice;
            if (device != null)
                return this.SetDefaultDevice(device);

            return false;
        }

        public bool SetDefaultCommunicationsDevice(AudioDevice dev)
        {
            var device = dev as DebugAudioDevice;
            if (device != null)
                return this.SetDefaultDevice(device);

            return false;
        }
    }
}
