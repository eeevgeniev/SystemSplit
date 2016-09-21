namespace SystemSplit.Engines
{
    using System;
    using System.Linq;
    using CommandExecuters;
    using IO;
    using Models.CustomSystems;

    public class Engine
    {
        private CustomSystem customSystem;
        private ConsoleReaderWritter consoleReaderWritter;
        private CommandExecuter commandExecuter;

        public Engine(CustomSystem customSystem, ConsoleReaderWritter consoleReaderWriter, 
            CommandExecuter commandExecuter)
        {
            this.CustomSystem = customSystem;
            this.ConsoleReaderWritter = consoleReaderWriter;
            this.CommandExecuter = commandExecuter;
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

        protected CommandExecuter CommandExecuter
        {
            get { return this.commandExecuter; }

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Command executer cannot be null.");
                }

                this.commandExecuter = value;
            }
        }

        public virtual void Run()
        {
            string command = string.Empty;

            while (true)
            {
                command = this.ConsoleReaderWritter.Read();

                if (command.Contains("("))
                {
                    string[] commandParameters = command.Split('(');

                    string currentCommand = commandParameters[0].Trim();

                    string parametersAsString = commandParameters[1].Replace(")", string.Empty).Trim();

                    string[] parameters = parametersAsString
                        .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToArray();

                    this.CommandExecuter.ExecuteCommand(currentCommand, parameters);
                }
                else
                {
                    string currentCommand = command.Trim();

                    if (currentCommand == "System Split")
                    {
                        this.CommandExecuter.ExecuteCommand(currentCommand, new string[] { });

                        break;
                    }
                }
            }
        }
    }
}
