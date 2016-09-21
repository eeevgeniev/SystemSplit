namespace SystemSplit.Models.CustomSystems
{
    using System.Collections.Generic;
    using System.Text;
    using Components.Bases;

    public class CustomSystem
    {
        private readonly Dictionary<string, HardwareComponent> hardwareByName;

        private readonly Dictionary<string, HardwareComponent> dumpedHardwareByName;

        public CustomSystem()
        {
            this.hardwareByName = new Dictionary<string, HardwareComponent>();
            this.dumpedHardwareByName = new Dictionary<string, HardwareComponent>();
        }

        public int MaxCapacity { get; private set; }

        public int MaxMemory { get; private set; }

        public int CapacityInUse { get; private set; }

        public int MemoryInUse { get; private set; }

        public int AddedSoftware { get; private set; }

        public void AddHardware(HardwareComponent hardware)
        {
            this.MaxCapacity += hardware.MaxCapacity;
            this.MaxMemory += hardware.MaxMemory;

            this.hardwareByName.Add(hardware.Name, hardware);
        }

        public void AddSoftwareToHardware(string hardwareName, SoftwareComponent software)
        {
            HardwareComponent hardware = null;

            if (this.hardwareByName.TryGetValue(hardwareName, out hardware))
            {
                if (hardware.AddSoftware(software))
                {
                    this.CapacityInUse += software.CapacityConsumation;
                    this.MemoryInUse += software.MemoryConsumation;

                    this.AddedSoftware++;
                } 
            }
        }

        public void RemoveSoftwareFromHardware(string hardwareName, string softwareName)
        {
            HardwareComponent hardware = null;

            if (this.hardwareByName.TryGetValue(hardwareName, out hardware))
            {
                int capacityBeforeRemoving = hardware.CurrentCapacity;
                int memoryBeforeRemoving = hardware.CurrentMemory;

                if (hardware.RemoveSoftware(softwareName))
                {
                    this.CapacityInUse -= (capacityBeforeRemoving - hardware.CurrentCapacity);
                    this.MemoryInUse -= (memoryBeforeRemoving - hardware.CurrentMemory);

                    this.AddedSoftware--;
                }
            }
        }

        public string SystemSplit()
        {
            StringBuilder buider = new StringBuilder();

            foreach(string componentName in this.hardwareByName.Keys)
            {
                buider.AppendLine(this.hardwareByName[componentName].ToString());
            }

            return buider.ToString();
        }

        public void Dump(string hardwareComponentName)
        {
            HardwareComponent hardware = null;

            if (this.hardwareByName.TryGetValue(hardwareComponentName, out hardware))
            {
                this.hardwareByName.Remove(hardwareComponentName);
                this.dumpedHardwareByName.Add(hardware.Name, hardware);

                this.MaxCapacity -= hardware.MaxCapacity;
                this.MaxMemory -= hardware.MaxMemory;

                this.MemoryInUse -= hardware.CurrentMemory;
                this.CapacityInUse -= hardware.CurrentCapacity;

                this.AddedSoftware -= hardware.GetSoftwareCount;
            }
        }

        public void Restore(string hardwareComponentName)
        {
            HardwareComponent hardware = null;

            if (this.dumpedHardwareByName.TryGetValue(hardwareComponentName, out hardware))
            {
                this.dumpedHardwareByName.Remove(hardwareComponentName);
                this.hardwareByName.Add(hardware.Name, hardware);

                this.MaxCapacity += hardware.MaxCapacity;
                this.MaxMemory += hardware.MaxMemory;

                this.MemoryInUse += hardware.CurrentMemory;
                this.CapacityInUse += hardware.CurrentCapacity;

                this.AddedSoftware += hardware.GetSoftwareCount;
            }
        }

        public void Destroy(string hardwareComponentName)
        {
            if (this.dumpedHardwareByName.ContainsKey(hardwareComponentName))
            {
                this.dumpedHardwareByName.Remove(hardwareComponentName);
            }
        }

        public string DumpAnalyze()
        {
            int countPowerComponents = 0;
            int countHeavyComponents = 0;
            int countExpressSoftware = 0;
            int countLightSoftware = 0;
            int dumpMemory = 0;
            int dumpCapacity = 0;

            foreach (string hardwareName in this.dumpedHardwareByName.Keys)
            {
                HardwareComponent component = this.dumpedHardwareByName[hardwareName];

                if (component.Type == "Power")
                {
                    countPowerComponents++;
                }
                else
                {
                    countHeavyComponents++;
                }

                countExpressSoftware += component.ExpressSoftwareCount;
                countLightSoftware += component.LightSoftwareCount;
                dumpMemory += component.CurrentMemory;
                dumpCapacity += component.CurrentCapacity;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Dump Analysis");
            builder.AppendLine($"Power Hardware Components: {countPowerComponents}");
            builder.AppendLine($"Heavy Hardware Components: {countHeavyComponents}");
            builder.AppendLine($"Express Software Components: {countExpressSoftware}");
            builder.AppendLine($"Light Software Components: {countLightSoftware}");
            builder.AppendLine($"Total Dumped Memory: {dumpMemory}");
            builder.Append($"Total Dumped Capacity: {dumpCapacity}");

            return builder.ToString();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("System Analysis");
            builder.AppendLine($"Hardware Components: {this.hardwareByName.Keys.Count}");
            builder.AppendLine($"Software Components: {this.AddedSoftware}");
            builder.AppendLine($"Total Operational Memory: {this.MemoryInUse} / {this.MaxMemory}");
            builder.Append($"Total Capacity Taken: {this.CapacityInUse} / {this.MaxCapacity}");

            return builder.ToString();
        }
    }
}
