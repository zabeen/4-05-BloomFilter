using NUnit.Framework;
using SpellChecker.Logic;
using System.Linq;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class BitmapTest
    {
        [Test]
        public void BitmapInitialisedCorrectly()
        {
            const int size = 10;
            var filter = new BloomFilter(size);
            Assert.AreEqual(size, filter.Bitmap.Length);
            Assert.IsTrue(filter.Bitmap.All(bit => bit == false));
        }

        [Test]
        public void BitValueCanBeChangedByIndex()
        {
            var filter = new BloomFilter(10);
            const int index = 1;

            Assert.IsFalse(filter.Bitmap[index]);

            filter.Bitmap[index] = true;
            Assert.IsTrue(filter.Bitmap[index]);

            filter.Bitmap[index] = false;
            Assert.IsFalse(filter.Bitmap[index]);
        }
    }
}
