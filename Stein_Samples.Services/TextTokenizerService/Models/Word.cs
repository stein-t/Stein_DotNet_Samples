namespace Samples.Services.TextTokenizerService.Models
{
    /// <summary>
    /// Model for Word properties
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Helper to display Error/Warnings/Information messages in the target UI control
        /// </summary>
        public string Message { get; set; }

        public string Value {get; set;}

        public int Length {get; set;}

        public Word(string value = null, int length = 0, string message = null)
        {
            Message = message;
            Value = value;
            Length = length;
        }
    }
}
