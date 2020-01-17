using Console_Tokenizer_Sample.Services;
using Stein_Samples.Services.TextTokenizerService;
using System;

namespace Console_Tokenizer_Sample
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads a text from user input, tokenizes it, produces a custom output
        /// see README.txt
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //retrieve the associated service
            ITokenizerService _TokenizerService = new TokenizerService();

            // put code above while loop that only needs to be executed once
            do
            {
                Console.WriteLine("#### Text Tokenizer - Willkommen ####");
                Console.Write("Enter some text: ");
                string text = Console.ReadLine();

                var result = _TokenizerService.Tokenize(text);      //retrive word tokens from the Tokenizer Service
                OutputService.Output(result, text);                       //trigger custom output from the Output service

                // get the user input for every iteration, allowing to exit at will
                Console.WriteLine("Continue (y/n)?");
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
