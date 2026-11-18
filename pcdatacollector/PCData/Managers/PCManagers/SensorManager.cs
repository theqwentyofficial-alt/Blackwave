using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Managers;
namespace pcdatacollector.PCData.Managers.PCManagers
{
    public static class SensorManager
    {
        public static List<ISensor> GetSensors(Computer computer, HardwareType hwType, string sensorName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
        public static List<ISensor> GetSensors(Computer computer, HardwareType hwType, string sensorName, string hardwName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName && i.Name == hardwName)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
        public static List<ISensor> GetSensors(Computer computer, HardwareType hwType, SensorType sType, string sensorName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName && sType == k.SensorType)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
    }
}
