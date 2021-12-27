using Console.Samples.Services;
using System;

namespace Console.Samples
{
    class Program
    {
        static void Main()
        {
            //retrieve the logging instance
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            Logger.Info("Application started");
            System.Console.WriteLine("#### Stein DotNet Samples - Willkommen ####");

            var serviceLocator = ServiceLocator.ServiceLocator.Create();

            // Menu
            do
            {
                System.Console.WriteLine($"(1) ... {TokenizerConsoleService.Title}");
                System.Console.WriteLine($"(2) ... {FileSystemCompareConsoleService.Title}");
                System.Console.WriteLine("(q) ... quit");

                var input = System.Console.ReadKey(true);

                // retrieve the associated service and start it or quit
                switch (input.Key)
                {
                    case ConsoleKey.Q:
                        break;
                    case ConsoleKey.D1:
                        IConsoleService tokenizerService = serviceLocator.Tokenizer;
                        tokenizerService.Start();
                        break;
                    case ConsoleKey.D2:
                        IConsoleService filesystemDiffSimulator = serviceLocator.FilesystemDiffSimulator;
                        filesystemDiffSimulator.Start();
                        break;
                    default:
                        System.Console.WriteLine("### Wrong Input! ###");
                        break;
                }
                if (input.Key == ConsoleKey.Q)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
