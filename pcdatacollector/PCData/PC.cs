using LibreHardwareMonitor.Hardware;
using Microsoft.Win32;
using pcdatacollector;
using System.Globalization;

namespace pcdatacollector.PCData
{
    public class PC
    {
        public CPU? cpu;
        public GPU? gpu;
        public MOTHERBOARD? motherboard;
        public RAM? ram;
        public WINDOWS? windows;
   public PC(CPU? cpu, GPU? gpu, MOTHERBOARD? motherBoard, RAM? ram, WINDOWS? windowsInfo)
        {
            GetPCInfo(cpu,gpu,motherBoard,ram,windowsInfo);
        }
        public PC(Computer computer)
        {
            GetPCInfo(computer);
        }
        private void GetPCInfo(Computer computer)
        {
            cpu = Helper.GetFullCpuInfo(computer);
            windows = new WINDOWS(
               Helper.GetComputerName(),
               Helper.GetHomeDrive(),
            Helper.GetUsername()
                );
            motherboard = Helper.GetFullBoardInfo(computer);
            gpu = new GPU();
            ram = new RAM(computer);
        }
        private void GetPCInfo(CPU? cpu, GPU? gpu, MOTHERBOARD? motherboard,RAM? ram, WINDOWS? windowsInfo)
        {
            this.cpu = cpu;
            this.gpu = gpu;
            this.motherboard = motherboard;
            this.ram = ram;
            this.windows = windowsInfo;
        }
    }
}
