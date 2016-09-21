namespace SystemSplit.Models.Components.Bases
{
    public abstract class SoftwareComponent : Component
    {
        private int capacityConsumation;
        private int memoryConsumation;

        protected SoftwareComponent(string name, string type, int capacityConsumation, int memoryConsumation)
            : base(name, type)
        {
            this.CapacityConsumation = capacityConsumation;
            this.MemoryConsumation = memoryConsumation;
        }

        public virtual int CapacityConsumation
        {
            get { return this.capacityConsumation; }

            protected set
            {
                this.capacityConsumation = value;
            }
        }

        public virtual int MemoryConsumation
        {
            get { return this.memoryConsumation; }

            protected set
            {
                this.memoryConsumation = value;
            }
        }
    }
}
