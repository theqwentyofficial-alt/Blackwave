using LibreHardwareMonitor.Hardware;
namespace DataProvider.Managers.PCManagers
{
    public static class SensorManager
    {
        public static List<ISensor> GetSensors(Computer computer, IHardware hw, string sensorName)
        {  
              List<ISensor> sensor = new List<ISensor>();
                  foreach (var sens in hw.Sensors)
                  {
                      if (sens.Name == sensorName)
                      {
                          sensor.Add(sens);
                      }
                  }
            return sensor;
        }
        public static List<ISensor> GetSensors(Computer computer, IHardware hw, string sensorName, string hardwName)
        {
            List<ISensor> sensor = new List<ISensor>();
                    foreach (var sens in hw.Sensors)
                    {
                        if (sens.Name == sensorName && hw.Name == hardwName)
                        {
                            sensor.Add(sens);
                        }
                    }
            return sensor;
        }
        public static List<ISensor> GetSensors(Computer computer, IHardware hw, SensorType sType, string sensorName)
        {
            List<ISensor> sensor = new List<ISensor>();

            foreach (var sens in hw.Sensors)
            {
                if (sens.Name == sensorName && sType == sens.SensorType)
                {
                    sensor.Add(sens);
                }
            }
            return sensor;
        }
        public static List<ISensor> GetSensors(Computer computer, IHardware hw, SensorType sType, string sensorName,string hardwName)
        {
            List<ISensor> sensor = new List<ISensor>();

            foreach (var sens in hw.Sensors)
            {
                if (sens.Name == sensorName && sType == sens.SensorType && hw.Name == hardwName)
                {
                    sensor.Add(sens);
                }
            }
            return sensor;
        }
    }
}
