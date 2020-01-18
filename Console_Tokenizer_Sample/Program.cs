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
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //retrieve the associated service
            ITokenizerService _TokenizerService = new TokenizerService();

            Console.WriteLine("#### Text Tokenizer - Willkommen ####");

            // iterate logic until user stops execution
            do
            {
                Console.Write("Enter some text: ");
                string text = Console.ReadLine();

                //retrieve result from the associated Service
                var result = _TokenizerService.TokenizeAndConvert(text);

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
