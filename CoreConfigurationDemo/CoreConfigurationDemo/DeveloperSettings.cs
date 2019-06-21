namespace CoreConfigurationDemo
{
    public class DeveloperSettings
    {
        public DeveloperSettings()
        {
            this.Address = new Address();
        }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Experience { get; set; }
        public Address Address { get; }
    }

    public class Address
    {
        public int HouseNo { get; set; }
        public string State { get; set; }
    }
}