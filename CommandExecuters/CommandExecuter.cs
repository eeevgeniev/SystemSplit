namespace SystemSplit.CommandExecuters
{
    using System;
    using Models.CustomSystems;
    using Models.Components.Hardware;
    using Models.Components.Software;
    using IO;

    public class CommandExecuter
    {
        private CustomSystem customSystem;
        private ConsoleReaderWritter consoleReaderWritter;

        public CommandExecuter(CustomSystem customSystem, ConsoleReaderWritter consoleReaderWriter)
        {
            this.CustomSystem = customSystem;
            this.ConsoleReaderWritter = consoleReaderWriter;
        }

        protected CustomSystem CustomSystem
        {
            get { return this.customSystem; }

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Custom system cannot be null.");
                }

                this.customSystem = value;
            }
        }

        protected ConsoleReaderWritter ConsoleReaderWritter
        {
            get { return this.consoleReaderWritter; }

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Reader writter cannot be null.");
                }

                this.consoleReaderWritter = value;
            }
        }

        public void ExecuteCommand(string command, string[] parameters)
        {
            switch (command)
            {
                case "RegisterPowerHardware":
                    RegisterPowerHardware(parameters[0], int.Parse(parameters[1]), int.Parse(parameters[2]));
                    break;
                case "RegisterHeavyHardware":
                    RegisterHeavyHardware(parameters[0], int.Parse(parameters[1]), int.Parse(parameters[2]));
                    break;
                case "RegisterExpressSoftware":
                    RegisterExpressSoftware(parameters[0], parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                    break;
                case "RegisterLightSoftware":
                    RegisterLightSoftware(parameters[0], parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                    break;
                case "ReleaseSoftwareComponent":
                    ReleaseSoftwareComponent(parameters[0], parameters[1]);
                    break;
                case "Analyze":
                    Analyze();
                    break;
                case "Dump":
                    Dump(parameters[0]);
                    break;
                case "Restore":
                    Restore(parameters[0]);
                    break;
                case "Destroy":
                    Destroy(parameters[0]);
                    break;
                case "DumpAnalyze":
                    DumpAnalyze();
                    break;
                case "System Split":
                    SystemSplit();
                    break;
            }
        }

        private void RegisterPowerHardware(string name, int maxCapacity, int maxMemory)
        {
            PowerHardware powerHardware = new PowerHardware(name, maxCapacity, maxMemory);

            this.CustomSystem.AddHardware(powerHardware);
        }

        private void RegisterHeavyHardware(string name, int maxCapacity, int maxMemory)
        {
            HeavyHardware heavyHardware = new HeavyHardware(name, maxCapacity, maxMemory);

            this.CustomSystem.AddHardware(heavyHardware);
        }

        private void RegisterExpressSoftware(string hardwareComponentName, string name,
            int capacityConsumation, int memoryConsumation)
        {
            ExpressSoftware expressSoftware = new ExpressSoftware(name, capacityConsumation, memoryConsumation);

            this.CustomSystem.AddSoftwareToHardware(hardwareComponentName, expressSoftware);
        }

        private void RegisterLightSoftware(string hardwareComponentName, string name,
            int capacityConsumation, int memoryConsumation)
        {
            LightSoftware lightSoftware = new LightSoftware(name, capacityConsumation, memoryConsumation);

            this.CustomSystem.AddSoftwareToHardware(hardwareComponentName, lightSoftware);
        }

        private void ReleaseSoftwareComponent(string hardwareComponentName, string softwareComponentName)
        {
            this.CustomSystem.RemoveSoftwareFromHardware(hardwareComponentName, softwareComponentName);
        }

        private void Analyze()
        {
            this.ConsoleReaderWritter.Write(this.CustomSystem.ToString());
        }

        private void SystemSplit()
        {
            this.ConsoleReaderWritter.Write(this.CustomSystem.SystemSplit());
        }

        private void Dump(string hardwareComponentName)
        {
            this.CustomSystem.Dump(hardwareComponentName);
        }

        private void Restore(string hardwareComponentName)
        {
            this.CustomSystem.Restore(hardwareComponentName);
        }

        private void Destroy(string hardwareComponentName)
        {
            this.CustomSystem.Destroy(hardwareComponentName);
        }

        private void DumpAnalyze()
        {
            this.ConsoleReaderWritter.Write(this.CustomSystem.DumpAnalyze());
        }
    }
}
