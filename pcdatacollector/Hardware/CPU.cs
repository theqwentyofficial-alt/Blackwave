namespace DataProvider.Hardware
{
    public class CPU
    {
        public string? Name { get; set; }
        public float? Load { get; set; }
        public float? CurrentMHZ { get; set; }
        public int? TotalCores { get; set; }
        public int? TotalThreads { get; set; }
        public string? Socket { get; set; }
        public ushort? MaxSpeed { get; set; }

        public CPU(string? name,int? cores, int? threads, string? socket, ushort? maxSpeed)
        {
            Name = name;
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
