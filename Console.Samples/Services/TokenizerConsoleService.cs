using NLog;
using Samples.Services.TextTokenizerService;
using System;
using System.Collections.Generic;

namespace Console.Samples.Services
{
    /// <summary>
    /// Reads a text from user input, tokenizes it, produces a custom output
    /// </summary>
    public class TokenizerConsoleService : IConsoleService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly string Title = "Text Tokenizer";

        private readonly ITokenizerService _tokenizerService;

        public TokenizerConsoleService(ITokenizerService tokenizerService)
        {
            _tokenizerService = tokenizerService;
        }

        public void Start()
        {
            System.Console.WriteLine($"### {Title} ###");

            // iterate logic until user stops execution
            do
            {
                System.Console.Write("Enter some text: ");
                string text = System.Console.ReadLine();

                IEnumerable<string> result;
                try
                {
                    // retrieve result from the associated Service
                    result = _tokenizerService.TokenizeAndConvert(text);
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.Message);       //Log Exception
                    throw;
                }

                // just forward to console
                foreach (var item in result)
                {
                    System.Console.WriteLine(item);
                }

                // get the user input for every iteration, allowing to exit at will
                System.Console.WriteLine("Continue [y|n]?");
                var input = System.Console.ReadKey(true);
                if (input.Key == ConsoleKey.N)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
