using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Components.Sensors;
using Model.Components.Workers;
using Model.Components.Inputs;
using Model.Components.Outputs;
using System.Timers;

namespace KettleController
{
    class Controller
    {
        IPowerSwitch powerSwitch;
        IPowerLamp powerLamp;
        ITemperatureSensor tempratureSensor;
        IWaterSensor waterSensor;
        IHeatingElement heatingElement;

        // A Timer is used to do continuous safety check while the controller is powered on.
        Timer timer = new Timer(1000);

        const int BOILING_POINT = 100;

        // The only public members of this Controller. They are used for testing.
        // Components of this controller should be private thus a public access of these components' working status should be provided for testing purposes.
        public bool PowerSwitchIsOn
        {
            get { return powerSwitch.IsOn; }
        }

        public bool PowerLampIsOn
        {
            get { return powerLamp.IsOn; }
        }

        public bool HeatingElementIsOn
        {
            get { return heatingElement.IsOn; }
        }

        public Controller(IPowerSwitch powerSwitch, IPowerLamp powerLamp, ITemperatureSensor tempratureSensor, IWaterSensor waterSensor, IHeatingElement heatingElement)
        {
            this.powerSwitch = powerSwitch;
            this.powerLamp = powerLamp;
            this.tempratureSensor = tempratureSensor;
            this.waterSensor = waterSensor;
            this.heatingElement = heatingElement;

            powerSwitch.SwitchedOn += powerSwitch_SwitchedOn;
            powerSwitch.SwitchedOff += powerSwitch_SwitchedOff;

            heatingElement.SwitchedOn += heatingElement_SwitchedOn;

            this.timer.Elapsed += timer_Elapsed;
            timer.Enabled = false;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Continuously check if kettle is opreating safely.
            if (heatingElement.IsOn)
            {
                SafetyCheck();
            }
        }

        void powerSwitch_SwitchedOff(object sender, EventArgs e)
        {
            // All components should be turned off when power switch is switched off.
            heatingElement.SwitchOff();
            powerLamp.SwitchOff();
            timer.Enabled = false;
        }

        void powerSwitch_SwitchedOn(object sender, EventArgs e)
        {
            // Does initial safety check before turning on heating when the power switch is switched on.
            if (SafetyCheck())
            {
                powerLamp.SwitchOn();
                heatingElement.SwitchOn();
            }
        }

        void heatingElement_SwitchedOn(object sender, EventArgs e)
        {
            // Countinuous safety check should be enable whenever heating element is on.
            timer.Enabled = true;
        }

        bool SafetyCheck()
        {
            if (waterSensor.CurrentValue) // Water is present
            {
                if (tempratureSensor.CurrentValue == int.MaxValue || tempratureSensor.CurrentValue == int.MinValue) // Faulty temperature sensor.
                {
                    powerSwitch.SwitchOff();
                    return false;
                }
                else if (tempratureSensor.CurrentValue >= BOILING_POINT) // Water reaches boiling point.
                {
                    powerSwitch.SwitchOff();
                    return false;
                }
                return true; // Conditions are normal, heating should be turned on when program executes this line.
            }
            else // No water inside kettle, should switch power off.
            {
                powerSwitch.SwitchOff();
                return false;
            }
        }
    }
}
