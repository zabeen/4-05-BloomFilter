using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public IEnumerable<int> Write(string value)
        {
            var hashes = GetHashIndexes(value).ToList();
            hashes.ForEach(h => Bitmap[h] = true);
            return hashes;
        }

        public bool Verify(string value)
        {
            return GetHashIndexes(value).All(i => Bitmap[i]);
        }

        private IEnumerable<int> GetHashIndexes(string value)
        {
            var byteArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value));

            // source: https://stackoverflow.com/a/5896716
            var result = new int[byteArray.Length / sizeof(int)];
            Buffer.BlockCopy(byteArray, 0, result, 0, result.Length);

            return result.Take(_hashCount);
        }
    }
}
