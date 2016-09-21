namespace SystemSplit.Models.Components.Software
{
    using Bases;

    public class ExpressSoftware : SoftwareComponent
    {
        private const string SoftwareType = "Express";

        public ExpressSoftware(string name, int capacityConsumation, int memoryConsumation)
            : base(name, SoftwareType, capacityConsumation, memoryConsumation)
        {
            this.MemoryConsumation = memoryConsumation * 2;
        }
    }
}
