namespace SystemSplit.Models.Components.Software
{
    using Bases;

    public class LightSoftware : SoftwareComponent
    {
        private const string SoftwareType = "Light";

        public LightSoftware(string name, int capacityConsumation, int memoryConsumation)
            : base(name, SoftwareType, capacityConsumation, memoryConsumation)
        {
            this.CapacityConsumation = (capacityConsumation + (capacityConsumation / 2));
            this.MemoryConsumation = (memoryConsumation - (memoryConsumation / 2));
        }
    }
}
