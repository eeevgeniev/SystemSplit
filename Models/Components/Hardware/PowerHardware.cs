namespace SystemSplit.Models.Components.Hardware
{
    using Bases;

    public class PowerHardware : HardwareComponent
    {
        private const string HardwareType = "Power";

        public PowerHardware(string name, int maxCapacity, int maxMemory)
            :base(name, HardwareType, maxCapacity, maxMemory)
        {
            this.MaxCapacity = maxCapacity - (maxCapacity * 3 / 4);
            this.MaxMemory = maxMemory + (maxMemory * 3 / 4);
        }
    }
}
