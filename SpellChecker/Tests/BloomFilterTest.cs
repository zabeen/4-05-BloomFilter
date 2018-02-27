using NUnit.Framework;
using SpellChecker.Logic;
using System.Linq;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class BloomFilterTest
    {
        private IBloomFilter _filter;
        private const int BitmapSize = 10;
        private const int HashCount = 5;

        [SetUp]
        public void BloomFilterSetup()
        {
           _filter = new BloomFilter(BitmapSize, HashCount);
        }

        [Test]
        public void BitmapInitialisedCorrectly()
        {
            Assert.AreEqual(BitmapSize, _filter.Bitmap.Length);
            Assert.IsTrue(_filter.Bitmap.All(bit => bit == false));
        }
    }
}
