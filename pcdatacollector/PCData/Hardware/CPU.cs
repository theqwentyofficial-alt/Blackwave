namespace pcdatacollector.PCData.Hardware
{
    public struct CPU
    {
        public string? Name { get; set; }
        public string? Vendor { get; set; }
        public int? GlobalMHZ { get; set; }
        public float? Load { get; set; }
        public float? CurrentMHZ { get; set; }
        public int? TotalCores { get; set; }
        public int? TotalThreads { get; set; }
        public string? Socket { get; set; }
        public ushort? MaxSpeed { get; set; }

        public CPU(string? name, string? vendor, int? globalMHZ, int? cores, int? threads, string? socket, ushort? maxSpeed)
        {
            Name = name;
            Vendor = vendor;
            GlobalMHZ = globalMHZ;
            TotalCores = cores;
            TotalThreads = threads;
            Socket = socket;
            MaxSpeed = maxSpeed;
        }
        public CPU()
        {

        }
    }
}
