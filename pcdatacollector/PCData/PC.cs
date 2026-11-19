using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Hardware;
using pcdatacollector.PCData.OS;
using pcdatacollector.PCData.Managers.HardwareManagers;
using pcdatacollector.PCData.Managers.OSManagers;
namespace pcdatacollector.PCData
{
    public class PC
    {
        public CPU? cpu;
        public GPU? gpu;
        public RAM? ram;
        public WINDOWS? windows;
   public PC(CPU? cpu, GPU? gpu, RAM? ram, WINDOWS? windowsInfo)
        {
            GetPCInfo(cpu,gpu,ram,windowsInfo);
        }
        public PC(Computer computer)
        {
            GetPCInfo(computer);
        }
        private void GetPCInfo(Computer computer)
        {
            cpu = CpuManager.GetFullInfo(computer);
            windows = new WINDOWS(
               WindowsManager.GetComputerName(),
               WindowsManager.GetHomeDrive(),
               WindowsManager.GetUsername()
                );
            gpu = new GPU();
            ram = new RAM(computer);
        }
        private void GetPCInfo(CPU? cpu, GPU? gpu,RAM? ram, WINDOWS? windowsInfo)
        {
           
            this.cpu = cpu;
            this.gpu = gpu;
            this.ram = ram;
            this.windows = windowsInfo;
        }
       
    }
}
