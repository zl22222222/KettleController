using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Components.Sensors;
using Model.Components.Workers;
using Model.Components.Inputs;
using Model.Components.Outputs;

namespace UnitTestKettleController
{
    class MockPowerSwitch : IPowerSwitch
    {
        public event EventHandler SwitchedOff;

        public event EventHandler SwitchedOn;

        private bool isOn = false;

        public void SwitchOn()
        {
            isOn = true;
            if (SwitchedOn != null)
            {
                SwitchedOn(this, new EventArgs());
            }
        }

        public void SwitchOff()
        {
            isOn = false;
            if (SwitchedOff != null)
            {
                SwitchedOff(this, new EventArgs());
            }
        }

        bool Model.Components.ISwitch.IsOn
        {
            get { return isOn; }
        }
    }

    class MockPowerLamp : IPowerLamp
    {
        public event EventHandler SwitchedOff;

        public event EventHandler SwitchedOn;

        private bool isOn = false;

        public void SwitchOn()
        {
            isOn = true;
            if (SwitchedOn != null)
            {
                SwitchedOn(this, new EventArgs());
            }
        }

        public void SwitchOff()
        {
            isOn = false;
            if (SwitchedOff != null)
            {
                SwitchedOff(this, new EventArgs());
            }
        }

        bool Model.Components.ISwitch.IsOn
        {
            get { return isOn; }
        }
    }

    class MockWaterSensor : IWaterSensor
    {
        public bool CurrentValue
        {
            get;
            set;
        }
    }

    class MockTemperatureSensor : ITemperatureSensor
    {
        public int CurrentValue
        {
            get;
            set;
        }
    }

    class MockHeatingElement : IHeatingElement
    {
        public event EventHandler SwitchedOff;

        public event EventHandler SwitchedOn;

        private bool isOn = false;

        public void SwitchOn()
        {
            isOn = true;
            if (SwitchedOn != null)
            {
                SwitchedOn(this, new EventArgs());
            }
        }

        public void SwitchOff()
        {
            isOn = false;
            if (SwitchedOff != null)
            {
                SwitchedOff(this, new EventArgs());
            }
        }

        bool Model.Components.ISwitch.IsOn
        {
            get { return isOn; }
        }
    }
}
