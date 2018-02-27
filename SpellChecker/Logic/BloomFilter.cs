namespace SpellChecker.Logic
{
    public class BloomFilter
    {
        public bool[] Bitmap { get; }
        private readonly int _hashCount;

        public BloomFilter(int bitmapSize, int hashCount = 5)
        {
            Bitmap = new bool[bitmapSize];
            _hashCount = hashCount;
        }
    }
}
