using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SpellChecker.Logic;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class SpellCheckerTest
    {
        private readonly ISpellCheck _check;
        private readonly IWordList _wordList;

        public SpellCheckerTest()
        {
            _wordList = new WordList(
                $"{TestContext.CurrentContext.TestDirectory}\\Tests\\words.txt");
            var filter = new BloomFilter();
            
            _check = new SpellCheck(filter, _wordList);
        }

        [Test]
        public void EveryBitInMapShouldNotBeTrue()
        {
            Assert.IsFalse(_check.Filter.Bitmap.All(bit => bit is true));
        }

        [Test]
        public void SpellCheckerVerifiesExistingWords()
        {
            var existing = new List<string> { "Abbottstown\'s", "overdries", "whatchamacallit", "uncliched" };
            foreach (var word in existing)
            {
                Assert.IsTrue(_wordList.Words.Contains(word));
                Assert.IsTrue(_check.WordExists(word));
            }
        }

        [Test]
        public void SpellCheckerRejectsNonExistingWord()
        {
            var nonExisting = new List<string> { "Zabeen", "Zubeida", "cliche" };
            foreach (var word in nonExisting)
            {
                Assert.IsFalse(_wordList.Words.Contains(word));
                Assert.IsFalse(_check.WordExists(word));
            }
        }
    }
}
