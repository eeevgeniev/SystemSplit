namespace SystemSplit.IO
{
    using System;

    public class ConsoleReaderWritter
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
