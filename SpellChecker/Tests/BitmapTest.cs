using NUnit.Framework;
using SpellChecker.Logic;
using System.Linq;

namespace SpellChecker.Tests
{
    [TestFixture]
    public class BitmapTest
    {
        private readonly IBitmap _filter;
        const int Size = 10;

        public BitmapTest()
        {
           _filter = new BloomFilter(Size);
        }

        [Test]
        public void BitmapInitialisedCorrectly()
        {
            Assert.AreEqual(Size, _filter.Bitmap.Length);
            Assert.IsTrue(_filter.Bitmap.All(bit => bit == false));
        }

        [Test]
        public void BitValueCanBeChangedByIndex()
        {
            const int index = 1;

            Assert.IsFalse(_filter.Bitmap[index]);

            _filter.Bitmap[index] = true;
            Assert.IsTrue(_filter.Bitmap[index]);

            _filter.Bitmap[index] = false;
            Assert.IsFalse(_filter.Bitmap[index]);
        }
    }
}
