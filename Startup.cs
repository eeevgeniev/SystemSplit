namespace SystemSplit
{
    using System;
    using CommandExecuters;
    using Engines;
    using Models.CustomSystems;
    using IO;

    public class Startup
    {
        public static void Main()
        {
            try
            {
                CustomSystem customSystem = new CustomSystem();
                ConsoleReaderWritter consoleReaderWriter = new ConsoleReaderWritter();
                CommandExecuter commandExecuter = new CommandExecuter(customSystem, consoleReaderWriter);

                Engine engine = new Engine(customSystem, consoleReaderWriter, commandExecuter);

                engine.Run();
            }
            catch (NullReferenceException nullRefEx)
            {
                Console.WriteLine(nullRefEx.Message);
            }
        }
    }
}