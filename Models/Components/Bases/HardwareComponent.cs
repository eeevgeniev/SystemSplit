namespace SystemSplit.Models.Components.Bases
{
    using System.Collections.Generic;
    using System.Text;

    public abstract class HardwareComponent : Component
    {
        private readonly Dictionary<string, SoftwareComponent> softwareComponentsByName;

        private int maxCapacity;
        private int maxMemory;
        private int currentCapacity;
        private int currentMemory;

        protected HardwareComponent(string name, string type, int maxCapacity, int maxMemory)
            : base(name, type)
        {
            this.softwareComponentsByName = new Dictionary<string, SoftwareComponent>();

            this.MaxCapacity = maxCapacity;
            this.MaxMemory = maxMemory;
            this.CurrentCapacity = 0;
            this.CurrentMemory = 0;
        }

        public virtual int MaxCapacity
        {
            get { return this.maxCapacity; }

            protected set
            {
                this.maxCapacity = value;
            }
        }

        public virtual int MaxMemory
        {
            get { return this.maxMemory; }

            protected set
            {
                this.maxMemory = value;
            }
        }

        public int CurrentCapacity
        {
            get { return this.currentCapacity; }

            protected set
            {
                this.currentCapacity = value;
            }
        }

        public int CurrentMemory
        {
            get { return this.currentMemory; }

            protected set
            {
                this.currentMemory = value;
            }
        }

        public int GetSoftwareCount
        {
            get
            {
                return this.softwareComponentsByName.Values.Count;
            }
        }

        protected Dictionary<string, SoftwareComponent> SoftwareComponentsByName => this.softwareComponentsByName;

        public int ExpressSoftwareCount { get; protected set; }

        public int LightSoftwareCount { get; protected set; }

        public virtual bool AddSoftware(SoftwareComponent software)
        {
            if (this.MaxCapacity < (this.CurrentCapacity + software.CapacityConsumation) || 
                this.MaxMemory < (this.CurrentMemory + software.MemoryConsumation))
            {
                return false;
            }

            this.CurrentCapacity += software.CapacityConsumation;
            this.CurrentMemory += software.MemoryConsumation;

            this.SoftwareComponentsByName.Add(software.Name, software);

            if (software.Type == "Express")
            {
                this.ExpressSoftwareCount++;
            }
            else
            {
                this.LightSoftwareCount++;
            }

            return true;
        }

        public virtual bool RemoveSoftware(string softwareName)
        {
            SoftwareComponent software = null;

            if (this.SoftwareComponentsByName.TryGetValue(softwareName, out software))
            {
                this.CurrentCapacity -= software.CapacityConsumation;
                this.CurrentMemory -= software.MemoryConsumation;

                this.SoftwareComponentsByName.Remove(softwareName);

                if (software.Type == "Express")
                {
                    this.ExpressSoftwareCount--;
                }
                else
                {
                    this.LightSoftwareCount--;
                }

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            int expressCount = 0;
            int lightCount = 0;
            List<string> softwareNames = new List<string>();

            foreach (string key in this.softwareComponentsByName.Keys)
            {
                SoftwareComponent software = this.softwareComponentsByName[key];

                if (software.Type == "Express")
                {
                    expressCount++;
                }
                else
                {
                    lightCount++;
                }

                softwareNames.Add(software.Name);
            }

            string softwareComponents = softwareNames.Count > 0 ? string.Join(", ", softwareNames) : "None";

            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Hardware Component - {this.Name}");

            builder.AppendLine($"Express Software Components - {expressCount}");
            builder.AppendLine($"Light Software Components - {lightCount}");
            builder.AppendLine($"Memory Usage: {this.CurrentMemory} / {this.MaxMemory}");
            builder.AppendLine($"Capacity Usage: {this.CurrentCapacity} / {this.MaxCapacity}");
            builder.AppendLine($"Type: {this.Type}");
            builder.Append($"Software Components: {softwareComponents}");

            return builder.ToString();
        }
    }
}
