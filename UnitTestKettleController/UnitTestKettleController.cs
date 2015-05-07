using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KettleController;
using System.Threading;

namespace UnitTestKettleController
{
    [TestClass]
    public class UnitTestKettleController
    {
        [TestMethod]
        public void TestNormalCondition()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            // Operating conditions
            mockTempratureSensor.CurrentValue = 20;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();

            // All components should be on
            bool expectedPowerSwitchIsOn = true;
            bool expectedPowerLampIsOn = true;
            bool expectedHeatingElementIsOn = true;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestWaterBoiling()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            // Operating conditions
            mockTempratureSensor.CurrentValue = 20;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();
            // Water reaches boiling point
            mockTempratureSensor.CurrentValue = 100;
            // Wait for safety check that runs every second
            Thread.Sleep(2000);

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestWaterAlreadyBoiled()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            // Water already over boiling point, could be water steam mixture.
            mockTempratureSensor.CurrentValue = 105;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestNoWater()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            mockTempratureSensor.CurrentValue = 20;
            // No water in kettle
            mockWaterSensor.CurrentValue = false;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestWaterEmptiedWhileHeating()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            mockTempratureSensor.CurrentValue = 20;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();
            Thread.Sleep(2000);
            // Water removed while heating
            mockWaterSensor.CurrentValue = false;
            Thread.Sleep(2000);

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestPowerSwitchOffWhileHeating()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            mockTempratureSensor.CurrentValue = 20;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();
            Thread.Sleep(2000);
            mockPowerSwitch.SwitchOff();

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestFaultyTemperatureSensorMax()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            // Faulty temperature sensor with max value
            mockTempratureSensor.CurrentValue = int.MaxValue;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestFaultyTemperatureSensorMin()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            // Faulty temperature sensor with min value
            mockTempratureSensor.CurrentValue = int.MinValue;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestFaultyTemperatureSensorWhileHeatingMax()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            mockTempratureSensor.CurrentValue = int.MaxValue;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();
            Thread.Sleep(2000);
            // Faulty temperature sensor with min value while heating
            mockTempratureSensor.CurrentValue = int.MaxValue;
            Thread.Sleep(2000);

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }

        [TestMethod]
        public void TestFaultyTemperatureSensorWhileHeatingMin()
        {
            MockPowerSwitch mockPowerSwitch = new MockPowerSwitch();
            MockPowerLamp mockPowerLamp = new MockPowerLamp();
            MockTemperatureSensor mockTempratureSensor = new MockTemperatureSensor();
            MockWaterSensor mockWaterSensor = new MockWaterSensor();
            MockHeatingElement mockHeatingElement = new MockHeatingElement();

            mockTempratureSensor.CurrentValue = int.MaxValue;
            mockWaterSensor.CurrentValue = true;

            Controller controller = new Controller(mockPowerSwitch, mockPowerLamp, mockTempratureSensor, mockWaterSensor, mockHeatingElement);

            mockPowerSwitch.SwitchOn();
            Thread.Sleep(2000);
            // Faulty temperature sensor with min value while heating
            mockTempratureSensor.CurrentValue = int.MinValue;
            Thread.Sleep(2000);

            // All components should be off
            bool expectedPowerSwitchIsOn = false;
            bool expectedPowerLampIsOn = false;
            bool expectedHeatingElementIsOn = false;
            bool actualPowerSwitchIsOn = controller.PowerSwitchIsOn;
            bool actualPowerLampIsOn = controller.PowerSwitchIsOn;
            bool actualHeatingElementIsOn = controller.PowerSwitchIsOn;

            Assert.AreEqual(expectedPowerSwitchIsOn, actualPowerSwitchIsOn);
            Assert.AreEqual(expectedPowerLampIsOn, actualPowerLampIsOn);
            Assert.AreEqual(expectedHeatingElementIsOn, actualHeatingElementIsOn);
        }
    }
}
