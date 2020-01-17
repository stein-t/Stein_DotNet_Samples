using Stein_Samples.Services.TextTokenizerService.Models;
using System;
using System.Collections.Generic;

namespace Stein_Samples.Services.TextTokenizerService
{
    /// <summary>
    /// Service class with methods for tokenizing a text
    /// Actually the service must not be static. Instead it would be registered in a Serviceprovider DI container to be consumed by the application with Dependency Injection!
    /// </summary>
    public class TokenizerService : ITokenizerService
    {
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
            IList<Word> words = new List<Word>();

            if (string.IsNullOrEmpty(text))
            {
                words.Add(new Word(message: "### No input provided! ###"));
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
                //provide warning
                words.Add(new Word(message: "### WARNING: Missing associated ending quotation mark! ###"));
            }
            else if (text.Length > start)
            {
                words.Add(new Word(value: text.Substring(start), length: text.Length - start));
            }

            return words;
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
