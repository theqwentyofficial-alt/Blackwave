namespace pcdatacollector.PCData
{
    public class MOTHERBOARD
    {
        public string? Name { get; set; }
        public string? Vendor { get; set; }
        public string? BIOSReleaseDate { get; set; }
        public string? Version { get; set; }
        public MOTHERBOARD(string? name, string? vendor, string? biosReleaseDate, string? version)
        {
            Name = name;
            Vendor = vendor;
            BIOSReleaseDate = biosReleaseDate;
            Version = version;
        }
        public MOTHERBOARD()
        {

        }
    }
}
