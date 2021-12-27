using Samples.Services.TextTokenizerService.Models;
using System.Collections.Generic;

namespace Samples.Services.TextTokenizerService
{
    /// <summary>
    /// Contract for the Tokenizer Service
    /// </summary>
    public interface ITokenizerService
    {
        /// <summary>
        /// Tokenizes the text and converts the word list into the appropriate format
        /// Instead the caller could also call both methods separately if to do some custom client-specific stuff with the Word list
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IEnumerable<string> TokenizeAndConvert(string text);

        /// <summary>
        /// Tokenizes the provided text:
        /// Text inside quotes or double quotes is counted as exactly one word 
        /// A ‘word’ is any combination of latin letters without whitespace in between. 
        /// Punctuation marks, slashes, currency signs., non-english letters etc. count as whitespace-separator. 
        /// Combinations of letters and numbers, e.g. “test123”, are not considered as words. 
        /// Exception: Everything inside quoted or double quoted text counts as a single word (excluding first and last quotation mark).
        /// </summary>
        /// <param name="text"></param>
        IEnumerable<Word> Tokenize(string text);

        /// <summary>
        /// converts the Word list into the appropriate target format
        /// </summary>
        /// <param name="words"></param>
        /// <param name="textInput"></param>
        IEnumerable<string> ConvertWordsToResult(IEnumerable<Word> words, string textInput);

        /// check for char is number
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool IsNumber(char c);

        /// <summary>
        /// Check for char is latin
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool IsLatinLetter(char c);
    }
}
