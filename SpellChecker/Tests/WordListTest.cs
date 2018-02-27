using NUnit.Framework;
using SpellChecker.Logic;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class WordListTest
    {
        private IWordList _wordList;

        [Test]
        public void WordFileSuccessfullyImportedWhenValidPathProvided()
        {
            var words = new List<string>();
            Assert.DoesNotThrow(() =>
            {
                var validPath = $"{TestContext.CurrentContext.TestDirectory}\\Tests\\words.txt";
                _wordList = new WordList(validPath);
                words = _wordList.Words.ToList();
            });

            Assert.IsTrue(words.Count > 0);
        }

        [Test]
        public void ExceptionThrownWhenInvalidPathProvided()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                var invalidPath = $"{TestContext.CurrentContext.TestDirectory}\\Tests\\foobar.txt";
                _wordList = new WordList(invalidPath);
            });
        }
    }
}
