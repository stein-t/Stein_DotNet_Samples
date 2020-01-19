using Stein_Samples.Services.TextTokenizerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stein_Samples.Services.TextTokenizerService
{
    /// <summary>
    /// Service Implementation with logic for providing tokenizing a text
    /// </summary>
    public class TokenizerService : ITokenizerService
    {
        //retrieve the logging instance
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Tokenizes the text and converts the word list into the appropriate format
        /// Instead the caller could also call both methods separately if to do some additional client-specific stuff with the Word list or format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IEnumerable<string> TokenizeAndConvert(string text)
        {
            return this.ConvertWordsToResult(this.Tokenize(text), text);
        }

        /// <summary>
        /// Tokenizes the provided text:
        /// Text inside quotes or double quotes is counted as exactly one word 
        /// A ‘word’ is any combination of latin letters without whitespace in between. 
        /// Punctuation marks, slashes, currency signs., non-english letters etc. count as whitespace-separator. 
        /// Combinations of letters and numbers, e.g. “test123”, are not considered as words. 
        /// Exception: Everything inside quoted or double quoted text counts as a single word (excluding first and last quotation mark).
        /// </summary>
        /// <param name="text"></param>
        public IEnumerable<Word> Tokenize(string text)
        {
            Logger.Debug(string.Concat("Input: ", text));         //Log text

            IList<Word> words = new List<Word>();

            if (string.IsNullOrEmpty(text))
            {
                Logger.Error("No input provided!");     //Log as Error
                words.Add(new Word(message: "### No input provided! ###"));
            }

            //check if to continue
            if (words.Any())
            {
                return words;
            }

            int start = 0;                      //holds the starting position of a potential word or quotation sequence
            char? isStartedQuotation = null;    //specifies if a new quotation sequence started by remembering the single or double quote sign

            //iterate text
            for (int pos = 0; pos < text.Length; pos++)
            {
                var c = text[pos];  //remember the char at the current position

                //test that no quotation sequence started
                if (isStartedQuotation == null) {
                    //test for latin char
                    if (!IsLatinLetter(c))
                    {
                        //test for number
                        if (!IsNumber(c))
                        {
                            if (pos > start) {
                                words.Add(new Word(value: text.Substring(start, pos - start), length: pos - start));
                            }
                        }
                        start = pos + 1;
                    }
                }

                //test for quotation marks
                if (IsQuotationMark(c, isStartedQuotation))
                {
                    if (isStartedQuotation != null)
                    {
                        //we reached the end of a quotation area
                        if (pos > start) {
                            words.Add(new Word(value: text.Substring(start, pos - start), length: pos - start));
                        }
                        isStartedQuotation = null;
                    }
                    else
                    {
                        //we start a new quotation area
                        isStartedQuotation = c;
                    }
                    start = pos + 1;
                }
            }

            if (isStartedQuotation != null)
            {
                //just provide the error
                words.Clear();
                words.Add(new Word(message: "### ERROR: Missing associated ending quotation mark! ###"));
                Logger.Error("Missing associated ending quotation mark");     //Log as Error
            }
            else if (text.Length > start)
            {
                words.Add(new Word(value: text.Substring(start), length: text.Length - start));
            }

            return words;
        }

        /// <summary>
        /// converts the Word list into the appropriate target format
        /// </summary>
        /// <param name="words"></param>
        /// <param name="textInput"></param>
        /// <returns></returns>
        public IEnumerable<string> ConvertWordsToResult(IEnumerable<Word> words, string textInput)
        {
            IList<string> result = new List<string>();

            var errorsExist = false;
            foreach (var w in words.Where(w => !string.IsNullOrEmpty(w.Message)))
            {
                //Display any information/warning/error messages
                result.Add(w.Message);
                errorsExist = true;
            }

            if (errorsExist)
            {
                //do not provide any further output
                return result;
            }

            //query all valid words
            var content = words.Where(w => string.IsNullOrEmpty(w.Message)).ToList();

            if (!string.IsNullOrEmpty(textInput))
            {
                var message = string.Format("This text has {0} words", content.Count());
                Logger.Debug(string.Concat("Success:", message));         //Log success
                result.Add(message);
            }

            if (content.Any())
            {
                var maxLength = content.Max(w => w.Length);       //retrieve the maximum length

                //query words by length
                for (int l = 1; l <= maxLength; l++)
                {
                    var wordsQuery = content.Where(w => w.Length == l).Select(w => w.Value);
                    if (!wordsQuery.Any())
                    {
                        //Uncomment if to output lenghts that do not occure
                        //result.Add(String.Format("words with {0} letter did not occur", l));
                    }
                    else
                    {
                        result.Add(String.Format("words with {0} letters occured {1} times (words={2})", l, wordsQuery.Count(), String.Join(", ", wordsQuery)));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Check for char is latin
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsLatinLetter(char c) {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        /// <summary>
        /// check for char is number
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsNumber(char c) => (c >= '1') && (c <= '9');

        /// <summary>
        /// check for char is (double or single) quotation mark
        /// optionally the quotationStart parameter can be provided to be matched against the c parameter
        /// </summary>
        /// <param name="c"></param>
        /// <param name="quotationStart"></param>
        /// <returns></returns>
        private bool IsQuotationMark(char c, char? quotationStart = null)
        {
            var charIsQuotationMark = c == '"' || c == '\'';

            if (charIsQuotationMark) {
                if (quotationStart != null)
                {
                    //check if c matches quotationStart
                    return c == quotationStart;
                }
                return true;
            }
            return false;
        }
    }
}
