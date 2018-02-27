using NUnit.Framework;
using SpellChecker.Logic;
using System.Linq;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class BloomFilterTest
    {
        private IBloomFilter _filter;
        private const int BitmapSize = byte.MaxValue;
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
            Assert.IsTrue(_filter.Bitmap.All(bit => bit is false));
        }

        [Test]
        public void CorrectNumberofBitsSetToTrue_OnlyAfterStringWrittenToMap()
        {
            const string written = "foo";
            const string notWritten = "bar";

            Assert.IsFalse(_filter.Verify(written));
            Assert.IsFalse(_filter.Verify(notWritten));

            var hashes = _filter.Write(written).ToList();
            Assert.AreEqual(HashCount, hashes.Count);
            hashes.ForEach(i => Assert.IsTrue(_filter.Bitmap[i]));

            Assert.IsTrue(_filter.Verify(written));            
            Assert.IsFalse(_filter.Verify(notWritten));
        }
    }
}
