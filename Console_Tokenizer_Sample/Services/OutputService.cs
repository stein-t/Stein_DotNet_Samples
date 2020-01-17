using Stein_Samples.Services.TextTokenizerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Tokenizer_Sample.Services
{
    /// <summary>
    /// Service class with methods for output
    /// Actually the service must not be static. Instead it would be registered in a Serviceprovider DI container to be consumed by the application with Dependency Injection!
    /// </summary>
    public static class OutputService
    {
        /// <summary>
        /// Prepare custom Console Output
        /// </summary>
        /// <param name="words"></param>
        public static void Output(IEnumerable<Word> result, string text)
        {
            foreach (var w in result.Where(w => !string.IsNullOrEmpty(w.Message)))
            {
                //Display any information/warning/error messages
                Console.WriteLine(w.Message);
                Console.WriteLine();                //Newline
            }

            //query all words
            var words = result.Where(w => string.IsNullOrEmpty(w.Message)).ToList();

            if (!string.IsNullOrEmpty(text))
            {
                Console.WriteLine(String.Format("This text has {0} words", words.Count()));
            }

            if (words.Any())
            {
                var maxLength = words.Max(w => w.Length);       //retrieve the maximum length

                //query words by length
                for (int l = 1; l <= maxLength; l++)
                {
                    var wordsQuery = words.Where(w => w.Length == l).Select(w => w.Value);
                    if (!wordsQuery.Any())
                    {
                        Console.WriteLine(String.Format("words with {0} letter did not occur", l));
                    }
                    else
                    {
                        Console.WriteLine(String.Format("words with {0} letters occured {1} times (words={2})", l, wordsQuery.Count(), String.Join(", ", wordsQuery)));
                    }
                }
            }



            //var amount = words.Where(w => string.IsNullOrEmpty(w.Message)).Count();
            //Console.WriteLine(String.Format("This text has {0} words:", amount));

            //var maxLength = words?.Max(w => w.Length) ?? 0;     //retrieve the maximum length
            //if (maxLength > 0)
            //{
            //    //query words by length
            //    for (int l = 1; l <= maxLength; l++)
            //    {
            //        var wordsQuery = words.Where(w => w.Length == l && string.IsNullOrEmpty(w.Message)).Select(w => w.Value);
            //        if (!wordsQuery.Any())
            //        {
            //            Console.WriteLine(String.Format("words with {0} letter did not occur", l));
            //        }
            //        else
            //        {
            //            Console.WriteLine(String.Format("words with {0} letters occured {1} times (words={2})", l, wordsQuery.Count(), String.Join(", ", wordsQuery)));
            //        }
            //    }
            //}
        }
    }
}
