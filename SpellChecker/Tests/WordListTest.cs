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
        [Test]
        public void WordFileSuccessfullyImportedWhenValidPathProvided()
        {
            var words = new List<string>();
            Assert.DoesNotThrow(() =>
            {
                var validPath = $"{TestContext.CurrentContext.TestDirectory}\\Tests\\words.txt";
                var wordList = new WordList(validPath);
                words = wordList.Words.ToList();
            });

            Assert.IsTrue(words.Count > 0);
        }

        [Test]
        public void ExceptionThrownWhenInvalidPathProvided()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                var invalidPath = $"{TestContext.CurrentContext.TestDirectory}\\Tests\\foobar.txt";
                var wordList = new WordList(invalidPath);
            });
        }
    }
}
