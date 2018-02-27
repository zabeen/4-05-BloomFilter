using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpellChecker.Logic
{
    public interface IBloomFilter
    {
        bool[] Bitmap { get; }
        IEnumerable<int> Write(string value);
        bool Verify(string value);
    }

    public class BloomFilter : IBloomFilter
    {
        public bool[] Bitmap { get; }
        private readonly int _hashCount;

        public BloomFilter(int bitmapSize, int hashCount)
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
