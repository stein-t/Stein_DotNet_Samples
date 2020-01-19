using Stein_Samples.Services.TextTokenizerService;
using System;
using System.Collections.Generic;

namespace Console_Tokenizer_Sample
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads a text from user input, tokenizes it, produces a custom output
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //retrieve the logging instance
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            Logger.Info("Application started");
            Console.WriteLine("#### Text Tokenizer - Willkommen ####");

            //retrieve the associated service
            ITokenizerService _TokenizerService = new TokenizerService();

            // iterate logic until user stops execution
            do
            {
                Console.Write("Enter some text: ");
                string text = Console.ReadLine();

                IEnumerable<string> result = null;

                try {
                    //retrieve result from the associated Service
                    result = _TokenizerService.TokenizeAndConvert(text);
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.Message);       //Log Exception
                    throw;
                }

                //just forward to console
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }

                // get the user input for every iteration, allowing to exit at will
                Console.Write("Continue [y|n]?");
                text = Console.ReadLine();
                if (text.Equals("n"))
                {
                    // exit the method.
                    break;
                }
            }
            while (true);
        }
    }
}
