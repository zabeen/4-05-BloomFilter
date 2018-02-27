using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellChecker.Logic
{
    public interface ISpellCheck
    {
        IBloomFilter Filter { get; }
        bool WordExists(string word);
    }

    public class SpellCheck : ISpellCheck
    {
        public IBloomFilter Filter { get; }

        public SpellCheck(IBloomFilter filter, IWordList wordList)
        {
            Filter = filter;

            foreach (var word in wordList.Words)
            {
                Filter.Write(word);
            }
        }

        public bool WordExists(string word)
        {
            return Filter.Verify(word);
        }
    }
}
