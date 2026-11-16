namespace pcdatacollector.PCData
{
    public class WINDOWS
    {
        public string? ComputerName { get; set; }
        public string? WindowsDrive { get; set; }
        public string? Username { get; set; }
        public string? OSVersion { get; set; }
        public bool Is64Bit { get; set; }

        public WINDOWS(string? computerName, string? windowsDrive, string? username)
        {
            ComputerName = computerName;
            WindowsDrive = windowsDrive;
            Username = username;
            OSVersion = Environment.OSVersion.Version.ToString();
            Is64Bit = Environment.Is64BitOperatingSystem;
        }
        public WINDOWS()
        {

        }
    }
}
