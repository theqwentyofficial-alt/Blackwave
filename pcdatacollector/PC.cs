using LibreHardwareMonitor.Hardware;
using DataProvider.Hardware;
using DataProvider.Managers.HardwareManagers;
namespace DataProvider
{
    public class PC
    {
        public CPU? cpu;
        public GPU? gpu;
        public RAM? ram;
   public PC(CPU? cpu, GPU? gpu, RAM? ram)
        {
            GetPCInfo(cpu,gpu,ram);
        }
        public PC(Computer computer)
        {
            GetPCInfo(computer);
        }
        private void GetPCInfo(Computer computer)
        {
            cpu = CpuManager.GetFullInfo(computer);
            gpu = new GPU();
            ram = new RAM(computer);
        }
        private void GetPCInfo(CPU? cpu, GPU? gpu,RAM? ram)
        {
           
            this.cpu = cpu;
            this.gpu = gpu;
            this.ram = ram;
        }
       
    }
}
