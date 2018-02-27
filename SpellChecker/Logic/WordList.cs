using System;
using System.Collections.Generic;
using System.IO;

namespace SpellChecker.Logic
{
    public interface IWordList
    {
        IEnumerable<string> Words { get; }
    }

    public class WordList : IWordList
    {
        public IEnumerable<string> Words { get; }

        public WordList(string filePath)
        {
            try
            {
                Words = File.ReadAllLines(filePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }         
        }
    }
}
