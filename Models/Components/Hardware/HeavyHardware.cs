namespace SystemSplit.Models.Components.Hardware
{
    using Bases;

    public class HeavyHardware : HardwareComponent
    {
        private const string HardwareType = "Heavy";

        public HeavyHardware(string name, int maxCapacity, int maxMemory)
            :base(name, HardwareType, maxCapacity, maxMemory)
        {
            this.MaxCapacity = (maxCapacity * 2);
            this.MaxMemory = (maxMemory - (maxMemory / 4));
        }
    }
}
